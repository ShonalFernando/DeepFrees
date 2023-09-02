using DeepFrees.CallDirecting.Model;

namespace DeepFrees.CallDirecting.Microservice
{
    public class CallDivetor
    {
        //Should Get From Database
        private List<CallCenterEmployee> employees = new List<CallCenterEmployee>
        {
            new CallCenterEmployee { EmpID = 1, Name = "Employee 1", IsAvailable = true, Category = "repairs" },
            new CallCenterEmployee { EmpID = 2, Name = "Employee 2", IsAvailable = true, Category = "consulting" },
            new CallCenterEmployee { EmpID = 3, Name = "Employee 3", IsAvailable = false, Category = "repairs" },
        };

        private int nextEmployeeIndex = 0;

        public CallCenterEmployee GetNextAvailableEmployee(string requestedCategory)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                int index = (nextEmployeeIndex + i) % employees.Count;
                if (employees[index].IsAvailable && employees[index].Category == requestedCategory)
                {
                    nextEmployeeIndex = (index + 1) % employees.Count;
                    return employees[index];
                }
            }

            return null;
        }
    }
}
