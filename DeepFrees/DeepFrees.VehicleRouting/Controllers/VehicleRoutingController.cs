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
        public async Task<IActionResult> Post([FromBody] List<DistanceMatrixModel> DistanceMatricesModel) 
        {
            RouteModel RouteModel = new();
            RouteModel.Depot = 1;
            RouteModel.VehicleNumber = 1;
            RouteModel.DistanceMatrices = DistanceMatricesModel;
            return Ok(_VehicleRouter.Shuffle(RouteModel));
        }

        [HttpPost("Route")]
        public async Task<IActionResult> PostLocation([FromBody] List<DistanceMatrixModel> DistanceMatricesModel)
        {
            RouteModel RouteModel = new();
            RouteModel.Depot = 1;
            RouteModel.VehicleNumber = 1;
            RouteModel.DistanceMatrices = DistanceMatricesModel;
            return Ok(_VehicleRouter.Shuffle(RouteModel));
        }
    }
}
