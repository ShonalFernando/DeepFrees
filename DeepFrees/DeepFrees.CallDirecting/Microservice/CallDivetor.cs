using DeepFrees.CallDirecting.Model;
using System.Collections.Generic;

namespace DeepFrees.CallDirecting.Microservice
{
    public class CallDivetor
    {
        public List<Call> CallPoolSolver (CallPool CallPool)
        {
            if(CallPool.EmpList != null && CallPool.CallList != null)
            {
                int nextEmployeeIndex = 0;

                List<CallAgent> CallAgents = CallPool.EmpList;
                List<Call> Calls = CallPool.CallList;

                List<Call> CallSolutions = new();

                foreach (var call in Calls)
                {
                    // Find an available agent that matches the category
                    var availableAgent = CallAgents.FirstOrDefault(
                        agent => agent.IsAvailable && agent.Category == call.RequestedCategory);

                    if (availableAgent != null)
                    {
                        // Assign the call to the agent
                        call.CallStatus = CallStatus.Started;
                        call.AssignedAgent = availableAgent.EmployeeID;
                        availableAgent.IsAvailable = false;
                    }
                    else
                    {
                        // If no available agent, mark the call as waiting
                        call.CallStatus = CallStatus.Waiting;
                    }

                    // Add the call to the list of assignments
                    CallSolutions.Add(call);
                }

                return CallSolutions;
            }
            else
            {
                return new List<Call>();
            }
            
        }
    }
}
