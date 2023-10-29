using Amazon.Runtime.Internal.Transform;
using DeepFrees.VehicleRouting.Helper;
using DeepFrees.VehicleRouting.MicroService;
using DeepFrees.VehicleRouting.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using Location = DeepFrees.VehicleRouting.Model.Location;

namespace DeepFrees.VehicleRouting.Controllers
{
    [Route("LocationService/[controller]")]
    [ApiController]
    public class VehicleRoutingController : ControllerBase
    {
        private readonly VehicleRouter _VehicleRouter;
        private readonly RouteDataContext _RouteDataContext;
        private readonly LocationDataContext _LocationDataContext;
        private readonly DistanceDataContext _DistanceDataContext;

        public VehicleRoutingController(DistanceDataContext distanceDataContext, VehicleRouter vehicleRouter, RouteDataContext routeDataContext, LocationDataContext locationDataContext)
        {
            _VehicleRouter = vehicleRouter;
            _RouteDataContext = routeDataContext;
            _LocationDataContext = locationDataContext;
            _DistanceDataContext = distanceDataContext;
        }

        [HttpGet("DummyGen")]
        public async Task<IActionResult> DummyGen() //This method returns a List of Locations
        {
            Random Random = new();
            var locss = await _LocationDataContext.GetAsync();
            var loocss = await _LocationDataContext.GetAsync();
            foreach (var locs in locss)
            {
                Dictionary<string, long> intds = new();
                foreach (var lecs in loocss)
                {
                    if (lecs.LocationID != locs.LocationID)
                    {
                        intds.Add(lecs.LocationID.ToString(), Random.Next(10, 500));
                        Console.WriteLine("1");
                    }
                    else
                    {
                        intds.Add(lecs.LocationID.ToString(), 0);
                        Console.WriteLine("2");
                    }
                }
                DistanceModel DistanceModel = new();
                DistanceModel.locationID = locs.LocationID;
                DistanceModel.distances = intds;
                DistanceModel._id = ObjectId.GenerateNewId();
                Console.WriteLine("4");

                await _DistanceDataContext.CreateAsync(DistanceModel);
            }
            return Ok();
        }

        //This is used to get the entire Distance Matrix : APPROVED
        [HttpGet("GetDistance")]
        public async Task<IActionResult> GetDistanceMatrix()
        {
            try
            {
                var DistanceMatrix = await _DistanceDataContext.GetAsync();
                if (DistanceMatrix != null)
                {
                    return Ok(DistanceMatrix);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        //This is used to get the entire Distance Matrix  : APPROVED
        [HttpGet("GetLocations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var Locations = await _LocationDataContext.GetAsync();
                var DistanceMatrix = await _DistanceDataContext.GetAsync();
                if (Locations != null)
                {
                    return Ok(Locations);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("Shuffle")] // : APPROVED
        public async Task<IActionResult> Shuffled()
        {
            var Distances = await _DistanceDataContext.GetAsync();
            if (Distances != null)
            {
                try
                {
                    RouteModel RouteModel = new();
                    RouteModel.Depot = 0;
                    RouteModel.VehicleNumber = 1;
                    RouteModel.DistanceMatrix = Distances;

                    return Ok(_VehicleRouter.Shuffle(RouteModel));
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetRoutes")] // : APPROVED
        public async Task<IActionResult> GetRoutes()
        {
            try
            {
                var Routes = await _RouteDataContext.GetAsync();
                if (Routes != null)
                {
                    return Ok(Routes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        //Create a new Route -> Shuffle and Save : APPROVED
        [HttpPost("CreateRoute")]
        public async Task<IActionResult> CreateRoute([FromBody] SavedRoute SavedRoute)
        {
            var Distances = await _DistanceDataContext.GetAsync();
            var Locations = await _LocationDataContext.GetAsync();
            var solModel = new SolutionMatrixModel();
            if (Distances != null && SavedRoute.vehicleIndex > 0)
            {
                try
                {
                    RouteModel RouteModel = new();
                    RouteModel.Depot = SavedRoute.startLocation;
                    RouteModel.VehicleNumber = SavedRoute.vehicleIndex;
                    RouteModel.DistanceMatrix = Distances;

                    solModel =  _VehicleRouter.Shuffle(RouteModel);
                    SavedRoute.totalDistance = solModel.TotalDistance.ToString();

                    SavedRoute.routeOrder = new int[solModel.RouteOrder.Count];
                    for (int i = 0; i < solModel.RouteOrder.Count; i++)
                    {
                        SavedRoute.routeOrder[i] = solModel.RouteOrder[i];
                    }

                    SavedRoute._id = ObjectId.GenerateNewId();

                    await _RouteDataContext.CreateAsync(SavedRoute);
                    await Console.Out.WriteLineAsync("Test1");
                    return Ok();
                }
                catch (Exception e)
                {
                    await Console.Out.WriteLineAsync("Test2 : " + e.Message);
                    return BadRequest();
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Test3" + SavedRoute.vehicleIndex);
                return NotFound();
            }

        }

        //Just add a new Location with ID : APPROVED
        [HttpPost("AddLocation")]
        public async Task<IActionResult> PostLocations([FromBody] Location Location)
        {
            try
            {
                var Locations = await _LocationDataContext.GetAsync();
                var locationWithLargestID = Locations.OrderByDescending(location => location.LocationID).FirstOrDefault();
                if (locationWithLargestID != null)
                {
                    Location.LocationID = locationWithLargestID.LocationID + 1;
                    Location._id = ObjectId.GenerateNewId();
                    var DupIDCheck = Locations.FindAll(l => l.LocationID.Equals(Location.LocationID));
                    if (!DupIDCheck.Any())
                    {
                        await _LocationDataContext.CreateAsync(Location);
                        return Ok(Location.LocationID);
                    }
                    else
                    {
                        Console.WriteLine("VRAL1: " + Location.LocationID);
                        return BadRequest();
                    }
                }
                else
                {
                    Console.WriteLine("VRAL2");
                    return NotFound();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return BadRequest(e);
            }
        }

        //Update previoud Distance and add a distance : APPROVED
        [HttpPost("AddDistance")]
        public async Task<IActionResult> PostDistance([FromBody] DistanceModel DistanceModel)
        {
            try
            {
                await _DistanceDataContext.CreateAsync(DistanceModel);

                var distances = await _DistanceDataContext.GetAsync();

                //update previous

                foreach (var distentry in distances)
                {
                    if (DistanceModel.distances != null && distentry.distances != null)
                    {
                        var TargetDistance = DistanceModel.distances[(distentry.locationID).ToString()];
                        distentry.distances.Add((DistanceModel.locationID).ToString(), TargetDistance); 
                    }
                    await _DistanceDataContext.UpdateAsync(distentry._id, distentry);
                }

                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Should Delete Both Location and Distance :: APPROVED
        [HttpDelete("DeleteLocation/{LocationID}")]
        public async Task<IActionResult> Delete(int LocationID)
        {
            try
            {
                int init = LocationID;

                //Get all data
                var locations = await _LocationDataContext.GetAsync();
                var distances = await _DistanceDataContext.GetAsync();

                //Jump locs
                for(int i = LocationID; i < locations.Count - 1; i++)
                {
                    if (locations[i].LocationID >= LocationID)
                    {
                        locations[i].City = locations[i + 1].City;

                        await _LocationDataContext.UpdateAsync(locations[i]._id, locations[i]);
                    }
                }

                //remove last
                await _LocationDataContext.RemoveAsync(locations.Count - 1);

                LocationID = init;

                //Jump dis
                for (int i = LocationID; i < distances.Count - 1; i++)
                {
                    if (distances[i].locationID >= LocationID)
                    {
                        distances[i].distances = distances[i + 1].distances;

                        await _DistanceDataContext.UpdateAsync(distances[i]._id, distances[i]);
                    }
                }

                //remove last
                await _DistanceDataContext.RemoveAsync(distances.Count - 1);

                //Get Data Again
                var distances2 = await _DistanceDataContext.GetAsync();

                foreach (var distdata in distances2)
                {
                    // Create a new dictionary
                    var newDistances = new Dictionary<string, long>();

                    for (int i = 0; i <= distdata.distances.Count - 2; i++)
                    {
                        string k = (i + 0).ToString();
                        string l = (i + 0 + 1).ToString();

                        // Check if both keys exist in the original dictionary
                        if (distdata.distances.ContainsKey(k) && distdata.distances.ContainsKey(l))
                        {
                            if(i>= LocationID)
                            {
                                // Swap values
                                newDistances[k] = distdata.distances[l];
                            }
                            else
                            {
                                newDistances[k] = distdata.distances[k];
                            }
                        }
                    }

                    // Update the distances dictionary with the new dictionary
                    distdata.distances = newDistances;

                    // Remove the last entry (key LocationID + distances.Count - 1)
                    distdata.distances.Remove((LocationID + distdata.distances.Count - 1).ToString());

                    // Update the DistanceModel with the new distances
                    await _DistanceDataContext.UpdateAsync(distdata._id, distdata);
                }                        
                return Ok();

            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}
