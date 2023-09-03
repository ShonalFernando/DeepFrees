using System.Collections.Generic;
using static DeepFreez.WebApp.Model.CallTechModel;

namespace DeepFreez.WebApp.Service
{
    public class CallCenterService
    {
        private readonly HttpClient _httpClient;

        public CallCenterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get
        public async Task<CallPoolSolution> GetPool(int id)
        {
            var cps = await _httpClient.GetFromJsonAsync<CallPoolSolution>($"api/mydata/{id}");
            if (cps != null)
            {
                return cps;
            }
            else
            {
                return new CallPoolSolution();
            }
        }

        // Create -> Process and Send Back
        public async Task<List<CallPoolSolution>> CreatePool(CallPool CallPool)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/mydata", CallPool);

            var cpsl = await _httpClient.GetFromJsonAsync<List<CallPoolSolution>>($"https://localhost:7041/api/callcenter");
            if (cpsl != null)
            {
                return cpsl;
            }
            else
            {
                return new List<CallPoolSolution>();
            }
        }

        // Delete : Not Implimented
        public async Task DeletePool(int id)
        {
            await _httpClient.DeleteAsync($"api/mydata/{id}");
        }

        // Update
        public async Task<List<CallPoolSolution>> UpdatePool(CallPool CallPool)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/mydata", CallPool);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content
                return await response.Content.ReadFromJsonAsync<List<CallPoolSolution>>();
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
