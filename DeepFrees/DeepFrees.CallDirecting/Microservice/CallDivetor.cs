using DeepFrees.CallDirecting.Model;
using System.Collections.Generic;

namespace DeepFrees.CallDirecting.Microservice
{
    public class CallDivetor
    {
        public static List<CallPoolSolution> CallPoolSolver (CallPool CallPool)
        {
            int nextEmployeeIndex = 0;
            List<CallCenterEmployee> employees = CallPool.EmpList;
            List<Call> Calls = CallPool.CallList;
            List<CallPoolSolution> CallPoolSolutions = new List<CallPoolSolution>();
            foreach (var call in Calls)
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    int index = (nextEmployeeIndex + i) % employees.Count;
                    if (employees[index].IsAvailable && employees[index].Category == call.RequestedCategory)
                    {
                        CallPoolSolution CallPoolSolution = new CallPoolSolution();
                        employees[index].IsAvailable = false;
                        nextEmployeeIndex = (index + 1) % employees.Count;
                        CallPoolSolution.CallID = call.CallID;
                        CallPoolSolution.EmpID = employees[index].EmpID;
                        CallPoolSolutions.Add(CallPoolSolution);
                    }
                }
            }

            return CallPoolSolutions;
        }
    }
}
