using DeepFrees.WebPro.Model;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace DeepFrees.WebPro.Services
{
    public class VRoutingStream
    {
        public async Task<List<DistanceModel>> GetAllDistances()
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7068/LocationService/VehicleRouting/GetDistance";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<DistanceModel>? Technicians = JsonSerializer.Deserialize<List<DistanceModel>>(content);
                        if (Technicians != null)
                        {

                            return Technicians;
                        }
                        else
                        {

                            return new List<DistanceModel>();
                        }
                    }
                    catch (Exception e)
                    {

                        await Console.Out.WriteLineAsync(e.Message);
                        return new List<DistanceModel>();
                    }
                }
                else
                {

                    return new List<DistanceModel>();
                }
            }
            else
            {

                return new List<DistanceModel>();
            }
        }

        public async Task<List<Location>> GetAllLocations()
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7068/LocationService/VehicleRouting/GetLocations";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<Location>? Technicians = JsonSerializer.Deserialize<List<Location>>(content);
                        if (Technicians != null)
                        {


                            return Technicians;
                        }
                        else
                        {

                            return new List<Location>();
                        }
                    }
                    catch (Exception e)
                    {

                        await Console.Out.WriteLineAsync(e.Message);
                        return new List<Location>();
                    }
                }
                else
                {

                    return new List<Location>();
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("2");
                return new List<Location>();
            }
        }
    }
}
