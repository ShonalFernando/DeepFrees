using DeepFrees.Dispatcher.Model;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace DeepFrees.Dispatcher.Microservice
{
    public class TaskTransformer
    {
        public int[,] TransformTasks(List<DispatchRequest> DispatchRequests)
        {
            // Identify distinct task categories
            var distinctTaskCategories = DispatchRequests.Select(dr => dr.TaskCategoryID).Distinct().OrderBy(id => id).ToList();

            // Create a 2D array
            int numRows = DispatchRequests.Select(dr => dr.EmployeeID).Distinct().Count();
            int numCols = distinctTaskCategories.Count();
            int[,] taskArray = new int[numRows, numCols];

            // Populate the array
            foreach (var request in DispatchRequests)
            {
                int rowIndex = Array.IndexOf(DispatchRequests.Select(dr => dr.EmployeeID).Distinct().ToArray(), request.EmployeeID);
                int colIndex = distinctTaskCategories.IndexOf(request.TaskCategoryID);

                taskArray[rowIndex, colIndex] = request.TaskPoints;
            }

            return taskArray;
        }
    }
}
