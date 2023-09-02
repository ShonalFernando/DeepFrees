using DeepFrees.Dispatcher.Microservice;
using DeepFrees.Dispatcher.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.Dispatcher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispatcherController : Controller
    {
        DispatchRequestList dispatchRequestList = new DispatchRequestList
        {
            dpList = new List<DispatchRequest>
            {
                new DispatchRequest { EmpID = 1, CatAvailableSlots = new int[] { 90, 80, 75, 70 } },
                new DispatchRequest { EmpID = 2, CatAvailableSlots = new int[] { 35, 85, 55, 65 } },
                new DispatchRequest { EmpID = 3, CatAvailableSlots = new int[] { 125, 95, 90, 95 } },
                new DispatchRequest { EmpID = 4, CatAvailableSlots = new int[] { 45, 110, 95, 115 } },
                new DispatchRequest { EmpID = 5, CatAvailableSlots = new int[] { 50, 100, 90, 100 } },
            },
            MaxCat = 4
        };

        [HttpPost]
        public IActionResult ShuffleSchedule(DispatchRequestList RequestList)
        {
            return Ok(DispatcherService.Shuffle(dispatchRequestList));
        }
    }
}
