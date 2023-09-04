﻿using DeepFrees.Scheduler.Model;
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

        public static WeeklyTaskSolutions Shuffle(WeeklyJob allJobs)
        {
            List<TaskSolution> solutions = new List<TaskSolution>();

            //var allJobs =
            //    new[] {
            //    new[] {
            //        // job0
            //        new { machine = 0, duration = 3 }, // task0
            //        new { machine = 1, duration = 2 }, // task1
            //        new { machine = 2, duration = 2 }, // task2
            //    }
            //        .ToList(),
            //    new[] {
            //        // job1
            //        new { machine = 0, duration = 2 }, // task0
            //        new { machine = 2, duration = 1 }, // task1
            //        new { machine = 1, duration = 4 }, // task2
            //    }
            //        .ToList(),
            //    new[] {
            //        // job2
            //        new { machine = 1, duration = 4 }, // task0
            //        new { machine = 2, duration = 3 }, // task1
            //    }
            //        .ToList(),
            //    }
            //        .ToList();

            int numMachines = 0;
            foreach (var job in allJobs.JobList)
            {
                foreach (var task in job.Tasks)
                {
                    numMachines = Math.Max(numMachines, 1 + task.Employee);
                }
            }
            int[] allMachines = Enumerable.Range(0, numMachines).ToArray();

            // Computes horizon dynamically as the sum of all durations.
            int horizon = 0;
            foreach (var job in allJobs.JobList)
            {
                foreach (var task in job.Tasks)
                {
                    horizon += task.Duration;
                }
            }

            // Creates the model.
            CpModel model = new CpModel();

            Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>> allTasks =
                new Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>>(); // (start, end, duration)
            Dictionary<int, List<IntervalVar>> machineToIntervals = new Dictionary<int, List<IntervalVar>>();
            for (int jobID = 0; jobID < allJobs.JobList.Count(); ++jobID)
            {
                var job = allJobs.JobList[jobID];
                for (int taskID = 0; taskID < job.Tasks.Count(); ++taskID)
                {
                    var task = job.Tasks[taskID];
                    String suffix = $"_{jobID}_{taskID}";
                    IntVar start = model.NewIntVar(0, horizon, "start" + suffix);
                    IntVar end = model.NewIntVar(0, horizon, "end" + suffix);
                    IntervalVar interval = model.NewIntervalVar(start, task.Duration, end, "interval" + suffix);
                    var key = Tuple.Create(jobID, taskID);
                    allTasks[key] = Tuple.Create(start, end, interval);
                    if (!machineToIntervals.ContainsKey(task.Employee))
                    {
                        machineToIntervals.Add(task.Employee, new List<IntervalVar>());
                    }
                    machineToIntervals[task.Employee].Add(interval);
                }
            }

            // Create and add disjunctive constraints.
            foreach (int machine in allMachines)
            {
                model.AddNoOverlap(machineToIntervals[machine]);
            }

            // Precedences inside a job.
            for (int jobID = 0; jobID < allJobs.JobList.Count(); ++jobID)
            {
                var job = allJobs.JobList[jobID];
                for (int taskID = 0; taskID < job.Tasks.Count() - 1; ++taskID)
                {
                    var key = Tuple.Create(jobID, taskID);
                    var nextKey = Tuple.Create(jobID, taskID + 1);
                    model.Add(allTasks[nextKey].Item1 >= allTasks[key].Item2);
                }
            }

            // Makespan objective.
            IntVar objVar = model.NewIntVar(0, horizon, "makespan");

            List<IntVar> ends = new List<IntVar>();
            for (int jobID = 0; jobID < allJobs.JobList.Count(); ++jobID)
            {
                var job = allJobs.JobList[jobID];
                var key = Tuple.Create(jobID, job.Tasks.Count() - 1);
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
                for (int jobID = 0; jobID < allJobs.JobList.Count(); ++jobID)
                {
                    var job = allJobs.JobList[jobID];
                    for (int taskID = 0; taskID < job.Tasks.Count(); ++taskID)
                    {
                        var task = job.Tasks[taskID];
                        var key = Tuple.Create(jobID, taskID);
                        int start = (int)solver.Value(allTasks[key].Item1);
                        if (!assignedJobs.ContainsKey(task.Employee))
                        {
                            assignedJobs.Add(task.Employee, new List<AssignedTask>());
                        }
                        assignedJobs[task.Employee].Add(new AssignedTask(jobID, taskID, start, task.Duration));
                    }
                }

                // Create per machine output lines.
                String output = "";
                foreach (int employee in allMachines)
                {
                    TaskSolution solution = new TaskSolution
                    {
                        Employee = employee
                    };

                    // Sort by starting time.
                    assignedJobs[employee].Sort();
                    String solLineTasks = $"Machine {employee}: ";
                    String solLine = "           ";

                    List<TaskSession> taskSessions = new List<TaskSession>();
                    foreach (var assignedTask in assignedJobs[employee])
                    {
                        TaskSession taskSession = new TaskSession();

                        String name = $"job_{assignedTask.jobID}_task_{assignedTask.taskID}";
                        // Add spaces to output to align columns.
                        solLineTasks += $"{name,-15}";
                        taskSession.JobID = assignedTask.jobID;
                        taskSession.TaskID = assignedTask.taskID;

                        String solTmp = $"[{assignedTask.start},{assignedTask.start + assignedTask.duration}]";
                        // Add spaces to output to align columns.
                        solLine += $"{solTmp,-15}";
                        taskSession.TaskStart = assignedTask.start;
                        taskSession.TaskEnd = assignedTask.duration + assignedTask.start;

                        taskSessions.Add(taskSession);
                    }
                    solution.TaskList = taskSessions;
                    output += solLineTasks + "\n";
                    output += solLine + "\n";

                    solutions.Add(solution);
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

            return new WeeklyTaskSolutions()
            {
                TaskSolutions = solutions,
                WeekID = allJobs.WeekID
            };
        }
    }
}