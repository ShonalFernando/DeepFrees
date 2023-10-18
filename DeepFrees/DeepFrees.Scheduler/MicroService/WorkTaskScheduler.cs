using DeepFrees.Scheduler.Model;
using Google.OrTools.Sat;
using OperationsResearch;

namespace DeepFrees.Scheduler.MicroService
{
    public class WorkTaskScheduler
    {
        private class AssignedTask : IComparable
        {
            public int jobID;
            public int taskID;
            public int start;
            public int duration;

            public AssignedTask(int jobID, int taskID, int start, int duration)
            {
                this.jobID = jobID;
                this.taskID = taskID;
                this.start = start;
                this.duration = duration;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                    return 1;

                AssignedTask otherTask = obj as AssignedTask;
                if (otherTask != null)
                {
                    if (this.start != otherTask.start)
                        return this.start.CompareTo(otherTask.start);
                    else
                        return this.duration.CompareTo(otherTask.duration);
                }
                else
                    throw new ArgumentException("Object is not a Temperature");
            }
        }

        public void Shuffle(JobScheduleRequest JobScheduleRequest)
        {
            var allJobs = JobScheduleRequest.AllJobs;

            int numteams = 0;
            foreach (var job in allJobs)
            {
                foreach (var task in job)
                {
                    numteams = Math.Max(numteams, 1 + task.team);
                }
            }
            int[] allteams = Enumerable.Range(0, numteams).ToArray();

            // Computes horizon dynamically as the sum of all durations.
            int horizon = 0;
            foreach (var job in allJobs)
            {
                foreach (var task in job)
                {
                    horizon += task.duration;
                }
            }

            // Creates the model.
            CpModel model = new CpModel();

            Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>> allTasks =
                new Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>>(); // (start, end, duration)
            Dictionary<int, List<IntervalVar>> teamToIntervals = new Dictionary<int, List<IntervalVar>>();
            for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
            {
                var job = allJobs[jobID];
                for (int taskID = 0; taskID < job.Count(); ++taskID)
                {
                    var task = job.Tasks[taskID];
                    String suffix = $"_{jobID}_{taskID}";
                    IntVar start = model.NewIntVar(0, horizon, "start" + suffix);
                    IntVar end = model.NewIntVar(0, horizon, "end" + suffix);
                    IntervalVar interval = model.NewIntervalVar(start, task.duration, end, "interval" + suffix);
                    var key = Tuple.Create(jobID, taskID);
                    allTasks[key] = Tuple.Create(start, end, interval);
                    if (!teamToIntervals.ContainsKey(task.team))
                    {
                        teamToIntervals.Add(task.team, new List<IntervalVar>());
                    }
                    teamToIntervals[task.team].Add(interval);
                }
            }

            // Create and add disjunctive constraints.
            foreach (int team in allteams)
            {
                model.AddNoOverlap(teamToIntervals[team]);
            }

            // Precedences inside a job.
            for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
            {
                var job = allJobs[jobID];
                for (int taskID = 0; taskID < job.Count() - 1; ++taskID)
                {
                    var key = Tuple.Create(jobID, taskID);
                    var nextKey = Tuple.Create(jobID, taskID + 1);
                    model.Add(allTasks[nextKey].Item1 >= allTasks[key].Item2);
                }
            }

            // Makespan objective.
            IntVar objVar = model.NewIntVar(0, horizon, "makespan");

            List<IntVar> ends = new List<IntVar>();
            for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
            {
                var job = allJobs[jobID];
                var key = Tuple.Create(jobID, job.Count() - 1);
                ends.Add(allTasks[key].Item2);
            }
            model.AddMaxEquality(objVar, ends);
            model.Minimize(objVar);

            // Solve
            CpSolver solver = new CpSolver();
            CpSolverStatus status = solver.Solve(model);
            Console.WriteLine($"Solve status: {status}");

            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                Console.WriteLine("Solution:");

                Dictionary<int, List<AssignedTask>> assignedJobs = new Dictionary<int, List<AssignedTask>>();
                for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
                {
                    var job = allJobs[jobID];
                    for (int taskID = 0; taskID < job.Count(); ++taskID)
                    {
                        var task = job.Tasks[taskID];
                        var key = Tuple.Create(jobID, taskID);
                        int start = (int)solver.Value(allTasks[key].Item1);
                        if (!assignedJobs.ContainsKey(task.team))
                        {
                            assignedJobs.Add(task.team, new List<AssignedTask>());
                        }
                        assignedJobs[task.team].Add(new AssignedTask(jobID, taskID, start, task.duration));
                    }
                }

                // Create per team output lines.
                String output = "";
                foreach (int team in allteams)
                {
                    // Sort by starting time.
                    assignedJobs[team].Sort();
                    String solLineTasks = $"team {team}: ";
                    String solLine = "           ";

                    foreach (var assignedTask in assignedJobs[team])
                    {
                        String name = $"job_{assignedTask.jobID}_task_{assignedTask.taskID}";
                        // Add spaces to output to align columns.
                        solLineTasks += $"{name,-15}";

                        String solTmp = $"[{assignedTask.start},{assignedTask.start + assignedTask.duration}]";
                        // Add spaces to output to align columns.
                        solLine += $"{solTmp,-15}";
                    }
                    output += solLineTasks + "\n";
                    output += solLine + "\n";
                }
                // Finally print the solution found.
                Console.WriteLine($"Optimal Schedule Length: {solver.ObjectiveValue}");
                Console.WriteLine($"\n{output}");
            }
            else
            {
                Console.WriteLine("No solution found.");
            }

            Console.WriteLine("Statistics");
            Console.WriteLine($"  conflicts: {solver.NumConflicts()}");
            Console.WriteLine($"  branches : {solver.NumBranches()}");
            Console.WriteLine($"  wall time: {solver.WallTime()}s");
        }
    }
}