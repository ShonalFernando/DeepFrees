using DeepFrees.TechnicianService.Model;
using System.Text.Json;

namespace DeepFrees.WebPro.Services
{
    public class TechnicianStream
    {
        public async Task<List<Technician>> GetTechnicianAll()
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7106/TechnicianService/Technician/GetTechnicians";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<Technician> Technicians = JsonSerializer.Deserialize<List<Technician>>(content);
                        if (Technicians != null)
                        {
                            return Technicians;
                        }
                        else
                        {
                            return new List<Technician>();
                        }
                    }
                    catch (Exception)
                    {

                        return new List<Technician>();
                    }
                }
                else
                {
                    return new List<Technician>();
                }
            }
            else
            {
                return new List<Technician>();
            }
        }

        public async Task<Technician> GetTechnicianSingle(string NIC)
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7106/TechnicianService/Technician/GetTechnician/" + NIC;
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        Technician Technician = JsonSerializer.Deserialize<Technician>(content);
                        if (Technician != null)
                        {
                            return Technician;
                        }
                        else
                        {
                            return new Technician();
                        }
                    }
                    catch (Exception)
                    {

                        return new Technician();
                    }
                }
                else
                {
                    return new Technician();
                }
            }
            else
            {
                return new Technician();
            }
        }

        public async Task UpdateTechnician(Technician Technician)
        {
            string responseContent;
            string apiUrl = $"https://localhost:7106/TechnicianService/Technician/Update/" + Technician.nic;
            HttpClient client = new HttpClient();
            var response = await client.PutAsJsonAsync(apiUrl, Technician);

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

        public async Task Add(Technician Technician)
        { 
            try
            {
            string responseContent;
            string apiUrl = $"https://localhost:7106/TechnicianService/Technician/CreateTechnician/";
                HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(apiUrl, Technician);
                await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        //public async Task DeleteTechnician(Technician Technician)
        //{
        //    Technician.isRecycled = true;

        //    HttpClient httpClient = new HttpClient();

        //    string content = JsonSerializer.Serialize(Technician);
        //    string apiUrl = "https://localhost:7107/Technician/DeleteTechnician/" + Technician.nic;
        //    HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string resp = await response.Content.ReadAsStringAsync();
        //        // Handle the response data
        //    }
        //}
    }
}
