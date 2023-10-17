using DeepFrees.CallDirecting.Microservice;
using DeepFrees.CallDirecting.Model;
using DeepFreesAccountsServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.CallDirecting.Controllers
{
    [ApiController]
    [Route("api/callcenter")]
    public class CallDirectingController : Controller
    {
        private readonly CallDataService _DataService;
        private readonly CallDivetor _CallDivetor;

        public CallDirectingController(CallDataService callDataService, CallDivetor callDivetor)
        {
            _DataService = callDataService;
            _CallDivetor = callDivetor;
        }

        //The Post method is used to Submit Call Pools and Employees (Available or Not)
        //The Post method is the most important, acts as a stream to update employee availability and get the solutions
        [HttpPost]
        public async Task<IActionResult> Post(CallPool CallPool)
        {
           return Ok(_CallDivetor.CallPoolSolver(CallPool));
        }
    }
}
