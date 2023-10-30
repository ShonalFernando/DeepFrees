using DeepFrees.Scheduler.Model;
using System.Text.Json;

namespace DeepFrees.WebPro.Services
{
    public class JobsStream
    {
        public async Task AddWorkTask(JobTask WorkTask)
        {
            try
            {
                WorkTask.isAvailable = true;
                string apiUrl = "https://localhost:7296/api/Scheduling/CreatePost";
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(apiUrl, WorkTask);
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        public async Task Shuffle(JobTask WorkTask)
        {
            try
            {
                WorkTask.isAvailable = true;
                string apiUrl = "https://localhost:7296/api/Scheduling/Shuffle";
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(apiUrl, WorkTask);
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public async Task<List<JobTask>> GetJobs()
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7296/api/Scheduling/GetTasks";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<JobTask> Employees = JsonSerializer.Deserialize<List<JobTask>>(content);
                        if (Employees != null)
                        {
                            return Employees;
                        }
                        else
                        {
                            return new List<JobTask>();
                        }
                    }
                    catch (Exception)
                    {

                        return new List<JobTask>();
                    }
                }
                else
                {
                    return new List<JobTask>();
                }
            }
            else
            {
                return new List<JobTask>();
            }
        }
    }
}
