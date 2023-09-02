using Microsoft.AspNetCore.Mvc;
using System;
using Google.OrTools.Sat;
using DeepFrees.Dispatcher.Model;

namespace DeepFrees.Dispatcher.Microservice
{
    public class DispatcherService
    {
        public static List<DispatchSolutions> Shuffle(DispatchRequestList RequestList)
        {
            int[,] requestsarray = new int[RequestList.dpList.Count(), RequestList.MaxCat];


            int value = 1;

            for (int row = 0; row < RequestList.dpList.Count(); row++)
            {
                for (int col = 0; col < RequestList.MaxCat; col++)
                {
                    requestsarray[row, col] = RequestList.dpList[row].CatAvailableSlots[col];
                    value++;
                }
            }

            int numWorkers = RequestList.dpList.Count();
            int numTasks = RequestList.MaxCat;

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
                    obj.AddTerm((IntVar)x[worker, task], requestsarray[worker, task]);
                }
            }
            model.Minimize(obj);

            // Solve
            CpSolver solver = new CpSolver();
            CpSolverStatus status = solver.Solve(model);
            Console.WriteLine($"Solve status: {status}");

            List<DispatchSolutions> dispSolutions = new List<DispatchSolutions>();

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
                            Console.WriteLine($"Worker {i} assigned to task {j}. Cost: {requestsarray[i, j]}");
                            DispatchSolutions dispSolution = new DispatchSolutions()
                            {
                                EmpID = i,
                                TaskID = j
                            };

                            dispSolutions.Add(dispSolution);
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
            
            return dispSolutions;
        }
    }
}
