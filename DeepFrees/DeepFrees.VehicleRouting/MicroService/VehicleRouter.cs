using DeepFrees.VehicleRouting.Model;
using System.Collections.Generic;
using Google.OrTools.ConstraintSolver;
using DeepFrees.VehicleRouting.Helper;

namespace DeepFrees.VehicleRouting.MicroService
{
    public class VehicleRouter
    {
        RouteModel? _RouteModel;

        /// <summary>
        ///   Print the solution.
        /// </summary>
        static SolutionMatrixModel PrintSolution(in RoutingModel routing, in RoutingIndexManager manager, in Assignment solution)
        {
            List<int> RouteSolutions = new List<int>();
            SolutionMatrixModel SolutionMatrixModel = new();

            Console.WriteLine("Objective: {0} miles", solution.ObjectiveValue());
            // Inspect solution.
            Console.WriteLine("Route:");
            long routeDistance = 0;
            var index = routing.Start(0);
            while (routing.IsEnd(index) == false)
            {
                Console.Write("{0} -> ", manager.IndexToNode((int)index));
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);

                RouteSolutions.Add(manager.IndexToNode((int)index));
            }
            Console.WriteLine("{0}", manager.IndexToNode((int)index));
            Console.WriteLine("Route distance: {0}miles", routeDistance);
            SolutionMatrixModel.RouteOrder = RouteSolutions;
            SolutionMatrixModel.TotalDistance = routeDistance;

            return SolutionMatrixModel;
        }

        public SolutionMatrixModel Shuffle(RouteModel routeModel)
        {
            // Instantiate the data problem.
            _RouteModel = routeModel;

            // Create Routing Index Manager
            RoutingIndexManager manager =
                new RoutingIndexManager(DistanceMatrixConverter.ConvertDictionarytoLong(_RouteModel.DistanceMatrix).GetLength(0), _RouteModel.VehicleNumber, _RouteModel.Depot);

            // Create Routing Model.
            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) =>
            {
                // Convert from routing variable Index to
                // distance matrix NodeIndex.
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);
                return DistanceMatrixConverter.ConvertDictionarytoLong(_RouteModel.DistanceMatrix)[fromNode, toNode];
            });

            // Define cost of each arc.
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            // Setting first solution heuristic.
            RoutingSearchParameters searchParameters =
                operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            // Solve the problem.
            Assignment solution = routing.SolveWithParameters(searchParameters);

            // Print solution on console.
            return PrintSolution(routing, manager, solution);
        }
    }
}
