using DeepFrees.VehicleRouting.Helper;
using DeepFrees.VehicleRouting.MicroService;
using DeepFrees.VehicleRouting.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.VehicleRouting.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly VehicleRouter _VehicleRouter;

        public EmployeeController(VehicleRouter vehicleRouter)
        {
            _VehicleRouter = vehicleRouter;
        }

        [HttpPost("Route")]
        public async Task<IActionResult> Post(int VehicleNumber, int Depot,[FromBody] List<DistanceMatrixModel> DistanceMatricesModel) 
        {
            RouteModel RouteModel = new();
            RouteModel.Depot = Depot;
            RouteModel.VehicleNumber = VehicleNumber;
            RouteModel.DistanceMatrices = DistanceMatricesModel;
            _VehicleRouter.Shuffle(RouteModel);
            return Ok();
        }
    }
}
