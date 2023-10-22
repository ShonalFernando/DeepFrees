using DeepFrees.EmployeeService.Model;
using System.Text.Json;

namespace DeepFrees.WebPro.Services
{
    public class EmployeeStream
    {
        public async Task<List<Employee>> GetEmployeeAll()
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7107/Employee/GetEmployee";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(content);
                if (content != null)
                {
                    try
                    {
                        List<Employee> Employees = JsonSerializer.Deserialize<List<Employee>>(content);
                        if (Employees != null)
                        {
                            return Employees;
                        }
                        else
                        {
                            return new List<Employee>();
                        }
                    }
                    catch (Exception)
                    {

                        return new List<Employee>();
                    }
                }
                else
                {
                    return new List<Employee>();
                }
            }
            else
            {
                return new List<Employee>();
            }
        }
        public async Task<Employee> GetEmployeeSingle(string NIC)
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7107/Employee/GetEmployee/" + NIC;
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(content);
                if (content != null)
                {
                    try
                    {
                        Employee Employee = JsonSerializer.Deserialize<Employee>(content);
                        if (Employee != null)
                        {
                            return Employee;
                        }
                        else
                        {
                            return new Employee();
                        }
                    }
                    catch (Exception)
                    {

                        return new Employee();
                    }
                }
                else
                {
                    return new Employee();
                }
            }
            else
            {
                return new Employee();
            }
        }
    }
}
