using DeepFrees.WebPro.Model;
using System.Text.Json;

namespace DeepFrees.WebPro.Services
{
    public class DispatchSolutionStream
    {
        public async Task<List<DispatchSolutionView>> ShuffleDisSolutions(List<DispatchRequest> DispatchRequests)
        {
            string responseContent;
            string apiUrl = $"https://localhost:7256/DispatchSolver/Dispatcher/Shuffle/" + DateTime.Now.DayOfYear; //Day of Year;
            HttpClient client = new HttpClient();
            var response = await client.PutAsJsonAsync(apiUrl, DispatchRequests);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<DispatchSolutionView> DispatchSolutions = JsonSerializer.Deserialize<List<DispatchSolutionView>>(content);
                        if (DispatchSolutions != null)
                        {
                            return DispatchSolutions;
                        }
                        else
                        {
                            return new List<DispatchSolutionView>();
                        }
                    }
                    catch (Exception)
                    {

                        return new List<DispatchSolutionView>();
                    }
                }
                else
                {
                    return new List<DispatchSolutionView>();
                }
            }
            else
            {
                return new List<DispatchSolutionView>();
            }
        }

    }
}
