using DeepFrees.EmployeeService.Model;
using System.Net.Http;
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

        public async Task UpdateEmployee(Employee Employee)
        {
            string responseContent;
            string apiUrl = $"https://localhost:7107/Employee/Update/"+ Employee.nic;
            HttpClient client = new HttpClient();
            var response = await client.PutAsJsonAsync(apiUrl, Employee);

            if (response.IsSuccessStatusCode)
            {
                string resp = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(resp);
            }
            else
            {
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task Add(Employee Employee)
        {
            string responseContent;
            string apiUrl = $"https://localhost:7107/Employee/Update/" + Employee.nic;
            HttpClient client = new HttpClient();
            var response = await client.PutAsJsonAsync(apiUrl, Employee);
        }

            public async Task DeleteEmployee(Employee Employee)
        {
            Employee.isRecycled = true;

            HttpClient httpClient = new HttpClient();

            string content = JsonSerializer.Serialize(Employee);
            string apiUrl = "https://localhost:7107/Employee/DeleteEmployee/" + Employee.nic;
            HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string resp = await response.Content.ReadAsStringAsync();
                // Handle the response data
            }
        }
    }
}
