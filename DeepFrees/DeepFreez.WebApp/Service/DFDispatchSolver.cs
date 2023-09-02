using DeepFreez.WebApp.Model;
using System.Text.Json;

namespace DeepFreez.WebApp.Service
{
    public class DFDispatchSolver
    {
        public async Task<List<DispatchSolutions>> SendDataToApi(DispatchRequestList DispList)
        {
            string jsonData = JsonSerializer.Serialize(DispList);

            string apiUrl = "http://localhost:5001/predict";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    List<DispatchSolutions>? _DispatchSolutions = JsonSerializer.Deserialize<List<DispatchSolutions>>(responseContent);
                    return _DispatchSolutions;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
