using DeepFrees.CallDirecting.Microservice;
using DeepFrees.CallDirecting.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.CallDirecting.Controllers
{
    [ApiController]
    [Route("api/callcenter")]
    public class CallDirectingController : Controller
    {
        [HttpPost("receive-call")]
        public IActionResult ReceiveCall([FromBody] Call call)
        {
            CallDivetor cdv = new CallDivetor();
            CallCenterEmployee availableEmployee = cdv.GetNextAvailableEmployee(call.RequestedCategory);

            if (availableEmployee != null)
            {
                availableEmployee.IsAvailable = false;
                //Call Logging
                return Ok($"Call {call.CallID} assigned to {availableEmployee.Name}");
            }
            else
            {
                // No available
                return NotFound("No available employees with the requested category. Please try again later.");
            }
        }
    }
}
