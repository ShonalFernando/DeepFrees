using DeepFreez.WebApp.Model;

namespace DeepFreez.WebApp.Service
{
    public class PayrollService
    {
        private readonly HttpClient _httpClient;

        public PayrollService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // View
        public async Task<SallaryModel> GetAccount(int id)
        {
            var Emp = await _httpClient.GetFromJsonAsync<SallaryModel>($"api/mydata/{id}");
            if (Emp != null)
            {
                return Emp;
            }
            else
            {
                return new SallaryModel();
            }
        }

        // Create
        public async Task CreateAccount(PayrollService PayrollService)
        {
            await _httpClient.PostAsJsonAsync($"api/mydata/", PayrollService);
        }

        // Delete
        public async Task DeleteAccount(int id)
        {
            await _httpClient.DeleteAsync($"api/mydata/{id}");
        }

        // Update
        public async Task UpdateAccount(int id, PayrollService PayrollService)
        {
            await _httpClient.PutAsJsonAsync($"api/mydata/{id}", PayrollService);
        }
    }
}
