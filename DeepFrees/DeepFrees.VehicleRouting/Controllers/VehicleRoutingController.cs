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

        //This is used to get the entire Distance Matrix
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

        //This is used to get the entire Distance Matrix
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

        [HttpGet("Shuffle")]
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

                    return Ok( _VehicleRouter.Shuffle(RouteModel));
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

        //Need to update all the distancemodel collection and add a location to location collection
        [HttpPost("AddLocations")]
        public async Task<IActionResult> PostLocations([FromBody] Tuple<Location, DistanceModel> RequestRouteData)
        {
            try
            {
                var alldistances = await _DistanceDataContext.GetAsync();
                var alllocations = await _LocationDataContext.GetAsync();

                //Update all distancemodels
                foreach (var currentdist in alldistances) //current in db
                {
                    foreach (var newLocDistance in RequestRouteData.Item2.distances) //new request
                    {
                        if (newLocDistance.Key == currentdist.locationID.ToString())
                        {
                            currentdist.distances.Add(RequestRouteData.Item1.LocationID.ToString(), newLocDistance.Value);
                        }
                    }
                }

                //Add a distance Model
                await _DistanceDataContext.CreateAsync(RequestRouteData.Item2);

                //Add to location
                await _LocationDataContext.CreateAsync(RequestRouteData.Item1);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
