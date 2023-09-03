using DeepFreez.WebApp.Model;

namespace DeepFreez.WebApp.Service
{
    public class AccountsService
    {
        private readonly HttpClient _httpClient;

        public AccountsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // View
        public async Task<UserAccount> GetAccount(int id)
        {
            var Emp = await _httpClient.GetFromJsonAsync<UserAccount>($"api/mydata/{id}");
            if (Emp != null)
            {
                return Emp;
            }
            else
            {
                return new UserAccount();
            }
        }

        // Create
        public async Task CreateAccount(UserAccount UserAccount)
        {
            await _httpClient.PostAsJsonAsync($"api/mydata/", UserAccount);
        }

        // Delete
        public async Task DeleteAccount(int id)
        {
            await _httpClient.DeleteAsync($"api/mydata/{id}");
        }

        // Update
        public async Task UpdateAccount(int id, UserAccount UserAccount)
        {
            await _httpClient.PutAsJsonAsync($"api/mydata/{id}", UserAccount);
        }
    }
}
