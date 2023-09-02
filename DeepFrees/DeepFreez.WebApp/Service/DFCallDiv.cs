using DeepFreez.WebApp.Model;
using System.Text.Json;

namespace DeepFreez.WebApp.Service
{
    public class DFCallDiv
    {
        public async Task<string> SendDataToApi(List<Call> CallModel)
        {
            string jsonData = JsonSerializer.Serialize(CallModel);

            string apiUrl = "http://localhost:5001/predict";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    string? Responsestring = JsonSerializer.Deserialize<string>(responseContent);
                    return Responsestring;
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
