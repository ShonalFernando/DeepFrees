using DeepFreez.WebApp.Model;
using System.Text.Json;

namespace DeepFreez.WebApp.Service
{
    public class DFScheduler
    {
        public async Task<TaskSolution> SendDataToApi(List<Job> JobLists)
        {
            string jsonData = JsonSerializer.Serialize(JobLists);

            string apiUrl = "http://localhost:5001/predict";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    TaskSolution? taskSolution = JsonSerializer.Deserialize<TaskSolution>(responseContent);
                    return taskSolution;
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
