using DeepFreez.WebApp.Model;
using System.Text.Json;

namespace DeepFreez.WebApp.Service
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // View: Get data from the API
        public async Task<Employee> GetEmployee(string id)
        {
            var Emp = await _httpClient.GetFromJsonAsync<Employee>($"https://localhost:7107/api/Employee/{id}");
            if (Emp != null)
            {
                return Emp; 
            }
            else
            {
                return new Employee();
            }
        }

        // Create
        public async Task CreateEmployee(Employee Employee)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:7107/api/Employee", Employee);
        }

        // Delete
        public async Task DeleteEmployee(string NIC)
        {
            await _httpClient.DeleteAsync($"https://localhost:7107/api/Employee/{NIC}");
        }

        // Update
        public async Task UpdateEmployee(string NIC, Employee Employee)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7107/api/Employee/{NIC}", Employee);
        }
    }
}
