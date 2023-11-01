using DeepFrees.TaskService.Model;
using DeepFrees.TechnicianService.Model;
using DeepFrees.WebPro.Model;
using System.Text.Json;

namespace DeepFrees.WebPro.Services
{
    public class DispatchSolutionStream
    {
        public async Task<Tuple<List<WorkTask>, List<Technician>>> ShuffleDisSolutions()
        {
            string responseContent;
            string apiUrl = $"https://localhost:7210/DispatchSolver/Dispatcher/Shuffle/";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        Tuple<List<WorkTask>, List<Technician>>? DispatchSolutions = JsonSerializer.Deserialize<Tuple<List<WorkTask>, List<Technician>>>(content);
                        if (DispatchSolutions != null)
                        {
                            Console.WriteLine("1");
                            return DispatchSolutions;
                        }
                        else
                        {
                            Console.WriteLine("5");

                            return Tuple.Create(new List<WorkTask>(), new List<Technician>());
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("2");

                        await Console.Out.WriteLineAsync( e.Message  );
                        return Tuple.Create(new List<WorkTask>(), new List<Technician>());
                    }
                }
                else
                           
                {
                    Console.WriteLine("3");
                    return Tuple.Create(new List<WorkTask>(), new List<Technician>());
                }
            }
            else
            {
                Console.WriteLine("4");

                return Tuple.Create(new List<WorkTask>(), new List<Technician>());
            }
        }

    }
}
