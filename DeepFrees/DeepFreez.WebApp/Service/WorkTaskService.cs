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
        public async Task<WeeklyTaskSolutions> GetAccount(int id)
        {
            var Emp = await _httpClient.GetFromJsonAsync<WeeklyTaskSolutions>($"api/mydata/{id}");
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
        public async Task CreateAccount(WeeklyJob WeeklyJob)
        {
            await _httpClient.PostAsJsonAsync($"api/mydata/", WeeklyJob);
        }

        // Delete
        public async Task DeleteAccount(int id)
        {
            await _httpClient.DeleteAsync($"api/mydata/{id}");
        }

        // Update
        public async Task UpdateAccount(int id, WeeklyJob WeeklyJob)
        {
            await _httpClient.PutAsJsonAsync($"api/mydata/{id}", WeeklyJob);
        }
    }
}
