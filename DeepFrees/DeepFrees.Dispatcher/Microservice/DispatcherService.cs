using Microsoft.AspNetCore.Mvc;
using System;
using Google.OrTools.Sat;
using DeepFrees.Dispatcher.Model;

namespace DeepFrees.Dispatcher.Microservice
{
    public class DispatcherService
    {
        //When requesting tasks the tasks should be included in the formula with previous tasks of a certain employee
        // Eg: when calculating tasks: If a emp has already assigned 5*Formula, then request as 6
        public void Shuffle(int[,] TasksArray)
        {
            // Data.
        //    int[,] TasksArray = {
        //    { 90, 80, 75, 70 }, { 35, 85, 55, 65 }, { 125, 95, 90, 95 }, { 45, 110, 95, 115 }, { 50, 100, 90, 100 },
        //};
            int numWorkers = TasksArray.GetLength(0);
            int numTasks = TasksArray.GetLength(1);

            // Model.
            CpModel model = new CpModel();

            // Variables.
            BoolVar[,] x = new BoolVar[numWorkers, numTasks];
            // Variables in a 1-dim array.
            for (int worker = 0; worker < numWorkers; ++worker)
            {
                for (int task = 0; task < numTasks; ++task)
                {
                    x[worker, task] = model.NewBoolVar($"worker_{worker}_task_{task}");
                }
            }

            // Constraints
            // Each worker is assigned to at most one task.
            for (int worker = 0; worker < numWorkers; ++worker)
            {
                List<ILiteral> tasks = new List<ILiteral>();
                for (int task = 0; task < numTasks; ++task)
                {
                    tasks.Add(x[worker, task]);
                }
                model.AddAtMostOne(tasks);
            }

            // Each task is assigned to exactly one worker.
            for (int task = 0; task < numTasks; ++task)
            {
                List<ILiteral> workers = new List<ILiteral>();
                for (int worker = 0; worker < numWorkers; ++worker)
                {
                    workers.Add(x[worker, task]);
                }
                model.AddExactlyOne(workers);
            }

            // Objective
            LinearExprBuilder obj = LinearExpr.NewBuilder();
            for (int worker = 0; worker < numWorkers; ++worker)
            {
                for (int task = 0; task < numTasks; ++task)
                {
                    obj.AddTerm((IntVar)x[worker, task], TasksArray[worker, task]);
                }
            }
            model.Minimize(obj);

            // Solve
            CpSolver solver = new CpSolver();
            CpSolverStatus status = solver.Solve(model);
            Console.WriteLine($"Solve status: {status}");

            // Print solution.
            // Check that the problem has a feasible solution.
            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                Console.WriteLine($"Total cost: {solver.ObjectiveValue}\n");
                for (int i = 0; i < numWorkers; ++i)
                {
                    for (int j = 0; j < numTasks; ++j)
                    {
                        if (solver.Value(x[i, j]) > 0.5)
                        {
                            Console.WriteLine($"Worker {i} assigned to task {j}. Cost: {TasksArray[i, j]}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No solution found.");
            }

            Console.WriteLine("Statistics");
            Console.WriteLine($"  - conflicts : {solver.NumConflicts()}");
            Console.WriteLine($"  - branches  : {solver.NumBranches()}");
            Console.WriteLine($"  - wall time : {solver.WallTime()}s");
        }
    }
}
