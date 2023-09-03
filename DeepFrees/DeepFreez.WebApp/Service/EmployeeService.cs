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
            var Emp = await _httpClient.GetFromJsonAsync<Employee>($"api/mydata/{id}");
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
            await _httpClient.PostAsJsonAsync($"api/mydata/", Employee);
        }

        // Delete
        public async Task DeleteEmployee(string id)
        {
            await _httpClient.DeleteAsync($"api/mydata/{id}");
        }

        // Update
        public async Task UpdateEmployee(string id, Employee Employee)
        {
            await _httpClient.PutAsJsonAsync($"api/mydata/{id}", Employee);
        }
    }
}
