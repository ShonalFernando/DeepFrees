using DeepFreez.WebApp.Model;
using System.Threading.Tasks;

namespace DeepFreez.WebApp.Service
{
    public class DispatchService
    {
        private readonly HttpClient _httpClient;

        public DispatchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get
        public async Task<DispatchSolutions> GetAccount(int WeekID)
        {
            var Emp = await _httpClient.GetFromJsonAsync<DispatchSolutions>($"https://localhost:7256/api/Dispatcher/");
            if (Emp != null)
            {
                return Emp;
            }
            else
            {
                return new DispatchSolutions();
            }
        }

        // Create
        public async Task<DispatchSolutions> CreateAccount(DispatchRequestList DispatchRequestList)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:7256/api/Dispatcher/", DispatchRequestList);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7256/api/Dispatcher/", DispatchRequestList);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content
                return await response.Content.ReadFromJsonAsync<DispatchSolutions>();
            }
            else
            {
                // Handle error response
                // You can throw an exception or return an error model, depending on your API design
                throw new Exception("Error creating or updating data.");
            }
        }

        // Delete
        public async Task DeleteAccount(int WeekID)
        {
            await _httpClient.DeleteAsync($"https://localhost:7256/api/Dispatcher/");
        }

        // Update
        public async Task<DispatchSolutions> UpdateAccount(int WeekID, DispatchRequestList DispatchRequestList)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:7256/api/Dispatcher/", DispatchRequestList);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7256/api/Dispatcher/", DispatchRequestList);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content
                return await response.Content.ReadFromJsonAsync<DispatchSolutions>();
            }
            else
            {
                // Handle error response
                // You can throw an exception or return an error model, depending on your API design
                throw new Exception("Error creating or updating data.");
            }
        }
    }
}
