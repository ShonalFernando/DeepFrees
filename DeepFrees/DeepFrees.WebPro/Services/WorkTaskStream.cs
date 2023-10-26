using DeepFrees.TaskService.Model;
using MongoDB.Bson.IO;
using System.Text;
using System.Text.Json;

namespace DeepFrees.WebPro.Services
{
    public class WorkTaskStream
    {
        public async Task<List<WorkTask>> GetWorkTasksAll()
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7210/TaskService/WorkTask/GetTasks";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<WorkTask> WorkTasks = JsonSerializer.Deserialize<List<WorkTask>>(content);
                        if (WorkTasks != null)
                        {
                            return WorkTasks;
                        }
                        else
                        {
                            return new List<WorkTask>();
                        }
                    }
                    catch (Exception)
                    {

                        return new List<WorkTask>();
                    }
                }
                else
                {
                    return new List<WorkTask>();
                }
            }
            else
            {
                return new List<WorkTask>();
            }
        }

        public async Task<WorkTask> GetWorkTaskSingle(string NIC)
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7210/TaskService/WorkTask/GetTasks/" + NIC;
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        WorkTask WorkTask = JsonSerializer.Deserialize<WorkTask>(content);
                        if (WorkTask != null)
                        {
                            return WorkTask;
                        }
                        else
                        {
                            return new WorkTask();
                        }
                    }
                    catch (Exception)
                    {

                        return new WorkTask();
                    }
                }
                else
                {
                    return new WorkTask();
                }
            }
            else
            {
                return new WorkTask();
            }
        }

        public async Task UpdateWorkTask(WorkTask WorkTask)
        {
            string responseContent;
            string apiUrl = $"https://localhost:7210/TaskService/WorkTask/GetTasks" + WorkTask._id;
            HttpClient client = new HttpClient();
            var response = await client.PutAsJsonAsync(apiUrl, WorkTask);

            if (response.IsSuccessStatusCode)
            {
                string resp = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(resp);
            }
            else
            {
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task AddWorkTask(WorkTask WorkTask)
        {
            try
            {
                WorkTask.isAvailable = true;
                string apiUrl = "https://localhost:7210/TaskService/WorkTask/CreateTask";
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(apiUrl, WorkTask);
                Console.WriteLine("Test 3");
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
                Console.WriteLine("Test 4");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public async Task DeleteWorkTask(WorkTask WorkTask)
        {

        }
    }
}

