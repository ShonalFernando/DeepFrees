using DeepFreez.WebApp.Model;

namespace DeepFreez.WebApp.Service
{
    public class WorkTaskService
    {
        private readonly HttpClient _httpClient;

        public WorkTaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // View
        public async Task<WeeklyTaskSolutions> GetSolutions(int Weekid)
        {
            var Emp = await _httpClient.GetFromJsonAsync<WeeklyTaskSolutions>($"https://localhost:7296/api/Scheduling/{Weekid}");
            if (Emp != null)
            {
                return Emp;
            }
            else
            {
                return new WeeklyTaskSolutions();
            }
        }

        // Create
        public async Task<DispatchSolutions> CreateSolutions(WeeklyJob WeeklyJob)
        {
            await _httpClient.PostAsJsonAsync($"api/mydata/", WeeklyJob);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"https://localhost:7296/api/Scheduling/", WeeklyJob);

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
        public async Task DeleteSolution(int Weekid)
        {
            await _httpClient.DeleteAsync($"https://localhost:7296/api/Scheduling/{Weekid}");
        }

        // Update
        public async Task<DispatchSolutions> UpdateSolution(WeeklyJob WeeklyJob)
        {
            await _httpClient.PostAsJsonAsync($"https://localhost:7296/api/Scheduling/", WeeklyJob);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"https://localhost:7296/api/Scheduling/", WeeklyJob);

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
