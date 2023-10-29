using DeepFrees.WebPro.Model;
using DeepFrees.WebPro.Model.HelperModels;
using MongoDB.Bson.IO;
using System.Text;
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

        public async Task<List<DistanceModel>> GetLocationsAll()
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

        public async Task<int> CreateLocation(Location Location)
        {
            try
            {
                await Console.Out.WriteLineAsync("Test 1");
                string apiUrl = $"https://localhost:7068/LocationService/VehicleRouting/AddLocation";
                HttpClient client = new HttpClient();
                var response = await client.PostAsync(apiUrl, new StringContent(JsonSerializer.Serialize(Location), Encoding.UTF8, "application/json"));

                await Console.Out.WriteLineAsync("Test 2" + (await response.Content.ReadAsStringAsync()));

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response content to get the returned ID
                    int returnedId = int.Parse(await response.Content.ReadAsStringAsync());

                    // Return the ID
                    return returnedId;
                }
                else
                {
                    // Handle the case where the request was not successful
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                    return -1; // You can return an appropriate error code or value
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        public async Task CreateDistance(DistanceModel DistanceModel)
        {
            try
            {
                await Console.Out.WriteLineAsync("Test 1");
                string responseContent;
                string apiUrl = $"https://localhost:7068/LocationService/VehicleRouting/AddDistance";
                HttpClient client = new HttpClient();
                var response = await client.PostAsync(apiUrl, new StringContent(JsonSerializer.Serialize(DistanceModel), Encoding.UTF8, "application/json"));
                await Console.Out.WriteLineAsync("Test 2" + (await response.Content.ReadAsStringAsync()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public async Task CreateRoute(SavedRoute SavedRoute)
        {
            try
            {
                await Console.Out.WriteLineAsync("Test 1");
                string responseContent;
                string apiUrl = $"https://localhost:7068/LocationService/VehicleRouting/CreateRoute";
                HttpClient client = new HttpClient();
                var response = await client.PostAsync(apiUrl, new StringContent(JsonSerializer.Serialize(SavedRoute), Encoding.UTF8, "application/json"));
                await Console.Out.WriteLineAsync("Test 2" + (await response.Content.ReadAsStringAsync()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public async Task DeleteLocation(int LocationID)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                string apiUrl = "https://localhost:7068/LocationService/VehicleRouting/DeleteLocation/" + LocationID;
                HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl); response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync( e.Message );
                throw;
            }
        }

        public async Task<List<SavedRoute>> GetAllRoutes()
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7068/LocationService/VehicleRouting/GetRoutes";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<SavedRoute>? SavedRoutes = JsonSerializer.Deserialize<List<SavedRoute>>(content);
                        if (SavedRoutes != null)
                        {


                            return SavedRoutes;
                        }
                        else
                        {

                            return new List<SavedRoute>();
                        }
                    }
                    catch (Exception e)
                    {

                        await Console.Out.WriteLineAsync(e.Message);
                        return new List<SavedRoute>();
                    }
                }
                else
                {

                    return new List<SavedRoute>();
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("2");
                return new List<SavedRoute>();
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
