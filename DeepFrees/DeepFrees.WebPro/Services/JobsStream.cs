using DeepFrees.Scheduler.Model;
using System.Text.Json;

namespace DeepFrees.WebPro.Services
{
    public class JobsStream
    {
        public async Task AddJob(JobCollection JobCollection)
        {
            try
            {
                string apiUrl = "https://localhost:7296/api/Scheduling/AddNewWeekjobs";
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(apiUrl, JobCollection);
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }


        public async Task<List<AssignedJobs>> Shuffle()
        {
            try
            {
                string apiUrl = "https://localhost:7296/api/Scheduling/GetSchedules";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(apiUrl);
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        try
                        {
                            List<AssignedJobs> Employees = JsonSerializer.Deserialize<List<AssignedJobs>>(content);
                            if (Employees != null)
                            {
                                return Employees;
                            }
                            else
                            {
                                return new List<AssignedJobs>();
                            }
                        }
                        catch (Exception)
                        {

                            return new List<AssignedJobs>();
                        }
                    }
                    else
                    {
                        return new List<AssignedJobs>();
                    }
                }
                else
                {
                    return new List<AssignedJobs>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return new List<AssignedJobs>();
            }
        }

        public async Task<List<JobCollection>> GetJobs()
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
                        List<JobCollection> Employees = JsonSerializer.Deserialize<List<JobCollection>>(content);
                        if (Employees != null)
                        {
                            return Employees;
                        }
                        else
                        {
                            return new List<JobCollection>();
                        }
                    }
                    catch (Exception)
                    {

                        return new List<JobCollection>();
                    }
                }
                else
                {
                    return new List<JobCollection>();
                }
            }
            else
            {
                return new List<JobCollection>();
            }
        }
    }
}
