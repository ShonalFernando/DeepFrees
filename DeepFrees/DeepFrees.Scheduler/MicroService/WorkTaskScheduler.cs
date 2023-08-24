using DeepFrees.Scheduler.Model;
using Google.OrTools.Sat;

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

        public void Schedule(String[] args)
        {
            JobRequestModel jrmodel = new JobRequestModel();
            jrmodel.empCatogery = 0;
            jrmodel.taskDuration = 3; 
            
            JobRequestModel jrmodel1 = new JobRequestModel();
            jrmodel1.empCatogery = 1;
            jrmodel1.taskDuration = 2;

            JobRequestModel jrmodel2 = new JobRequestModel();
            jrmodel2.empCatogery = 2;
            jrmodel2.taskDuration = 2;

            JobRequestModel jrmodel3 = new JobRequestModel();
            jrmodel3.empCatogery = 0;
            jrmodel3.taskDuration = 2;

            JobRequestModel jrmodel4 = new JobRequestModel();
            jrmodel4.empCatogery = 2;
            jrmodel4.taskDuration = 1;

            JobRequestModel jrmodel5 = new JobRequestModel();
            jrmodel5.empCatogery = 1;
            jrmodel5.taskDuration = 4;

            JobRequestModel jrmodel6 = new JobRequestModel();
            jrmodel6.empCatogery = 1;
            jrmodel6.taskDuration = 4;

            JobRequestModel jrmodel7 = new JobRequestModel();
            jrmodel7.empCatogery = 2;
            jrmodel7.taskDuration = 3;

            List<JobRequestModel> jrmlist = new List<JobRequestModel>();
            jrmlist.Add(jrmodel);
            jrmlist.Add(jrmodel1);
            jrmlist.Add(jrmodel2);

            List<JobRequestModel> jrmlist1 = new List<JobRequestModel>();
            jrmlist1.Add(jrmodel3);
            jrmlist1.Add(jrmodel4);
            jrmlist1.Add(jrmodel5);

            List<JobRequestModel> jrmlist2 = new List<JobRequestModel>();
            jrmlist2.Add(jrmodel6);
            jrmlist2.Add(jrmodel7);

            List<List<JobRequestModel>> allJobs = new List<List<JobRequestModel>>();
            allJobs.Add(jrmlist);
            allJobs.Add(jrmlist1);
            allJobs.Add(jrmlist2);

            foreach(var aj in allJobs)
            {
                foreach(var a in aj)
                {
                    Console.WriteLine(a.empCatogery.ToString() + " : " + a.taskDuration.ToString());
                }
            }

            Console.WriteLine("CHECK");

            var allJobsa =
                new[] 
                {
                    new[] 
                    {
                        // job0
                        new { machine = 0, duration = 3 }, // task0
                        new { machine = 1, duration = 2 }, // task1
                        new { machine = 2, duration = 2 }, // task2
                    }.ToList(),

                    new[] 
                    {
                        // job1
                        new { machine = 0, duration = 2 }, // task0
                        new { machine = 2, duration = 1 }, // task1
                        new { machine = 1, duration = 4 }, // task2
                    }.ToList(),

                    new[] 
                    {
                        // job2
                        new { machine = 1, duration = 4 }, // task0
                        new { machine = 2, duration = 3 }, // task1
                    }.ToList(),
                }.ToList();

            foreach (var aj in allJobsa)
            {
                foreach (var a in aj)
                {
                    Console.WriteLine(a.machine.ToString() + " : " + a.duration.ToString());
                }
            }

            int numMachines = 0;
            foreach (var job in allJobs)
            {
                foreach (var task in job)
                {
                    numMachines = Math.Max(numMachines, 1 + task.empCatogery);
                }
            }
            int[] allMachines = Enumerable.Range(0, numMachines).ToArray();

            // Computes horizon dynamically as the sum of all durations.
            int horizon = 0;
            foreach (var job in allJobs)
            {
                foreach (var task in job)
                {
                    horizon += task.taskDuration;
                }
            }

            // Creates the model.
            CpModel model = new CpModel();

            Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>> allTasks =
                new Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>>(); // (start, end, duration)
            Dictionary<int, List<IntervalVar>> machineToIntervals = new Dictionary<int, List<IntervalVar>>();
            for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
            {
                var job = allJobs[jobID];
                for (int taskID = 0; taskID < job.Count(); ++taskID)
                {
                    var task = job[taskID];
                    String suffix = $"_{jobID}_{taskID}";
                    IntVar start = model.NewIntVar(0, horizon, "start" + suffix);
                    IntVar end = model.NewIntVar(0, horizon, "end" + suffix);
                    IntervalVar interval = model.NewIntervalVar(start, task.taskDuration, end, "interval" + suffix);
                    var key = Tuple.Create(jobID, taskID);
                    allTasks[key] = Tuple.Create(start, end, interval);
                    if (!machineToIntervals.ContainsKey(task.empCatogery))
                    {
                        machineToIntervals.Add(task.empCatogery, new List<IntervalVar>());
                    }
                    machineToIntervals[task.empCatogery].Add(interval);
                }
            }

            // Create and add disjunctive constraints.
            foreach (int machine in allMachines)
            {
                model.AddNoOverlap(machineToIntervals[machine]);
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

            List<EmployeeShedule> lsmscdhle = new List<EmployeeShedule>();

            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                Console.WriteLine("Solution:");

                Dictionary<int, List<AssignedTask>> assignedJobs = new Dictionary<int, List<AssignedTask>>();
                for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
                {
                    var job = allJobs[jobID];
                    for (int taskID = 0; taskID < job.Count(); ++taskID)
                    {
                        var task = job[taskID];
                        var key = Tuple.Create(jobID, taskID);
                        int start = (int)solver.Value(allTasks[key].Item1);
                        if (!assignedJobs.ContainsKey(task.empCatogery))
                        {
                            assignedJobs.Add(task.empCatogery, new List<AssignedTask>());
                        }
                        assignedJobs[task.empCatogery].Add(new AssignedTask(jobID, taskID, start, task.taskDuration));
                    }
                }

                

                // Create per machine output lines.
                String output = "";
                foreach (int machine in allMachines)
                {
                    // Sort by starting time.
                    assignedJobs[machine].Sort();
                    String solLineTasks = $"Employee {machine}: ";
                    String solLine = "           ";
                    

                    foreach (var assignedTask in assignedJobs[machine])
                    {
                        EmployeeShedule empschdl = new EmployeeShedule();
                    empschdl.EmpID = machine.ToString();

                        String name = $"job_{assignedTask.jobID}_task_{assignedTask.taskID}";
                        // Add spaces to output to align columns.
                        solLineTasks += $"{name,-15}";

                        String solTmp = $"[{assignedTask.start},{assignedTask.start + assignedTask.duration}]";
                        // Add spaces to output to align columns.
                        solLine += $"{solTmp,-15}";
                        empschdl.StartSpan = assignedTask.start;
                        empschdl.EndSpan = assignedTask.start + assignedTask.duration; ;

                        lsmscdhle.Add(empschdl);
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

            foreach(var eeeeee in lsmscdhle)
            {
                Console.WriteLine("Employee: " + eeeeee.EmpID + ", Time Start: " + eeeeee.StartSpan.ToString() + ", Time Complete:" + eeeeee.EndSpan.ToString()); ;
            }
        }
    }
}
