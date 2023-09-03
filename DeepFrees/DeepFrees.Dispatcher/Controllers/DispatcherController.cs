using DeepFrees.Dispatcher.Microservice;
using DeepFrees.Dispatcher.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.Dispatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatcherController : ControllerBase
    {
        private readonly DispatcherDataService _DispatcherDataService;

        public DispatcherController(DispatcherDataService dispatcherDataService)
        {
            _DispatcherDataService = dispatcherDataService;
        }

        //Get UserDetails
        [HttpGet("{TaskID}")]
        public async Task<IActionResult> Get(int WeekID)
        {
            var uacc = await _DispatcherDataService.GetAsync(WeekID);

            if (uacc is null)
            {
                return NotFound();
            }

            return Ok(uacc);
        }

        //Account Creation
        [HttpPost]
        public async Task<IActionResult> Post(DispatchRequestList DispatchRequestList)
        {
            if (_DispatcherDataService.GetAsync(DispatchRequestList.WeekID) == null)
            {
                try
                {
                    var DispatchSolutions = DispatcherService.Shuffle(DispatchRequestList, DispatchRequestList.WeekID);
                    await _DispatcherDataService.CreateAsync(DispatchSolutions);
                    return Ok(DispatchSolutions);
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                } 
            }
            else
            {
                return BadRequest("Weekly Scheduled Already Exist");
            }
        }

        //Account Update
        [HttpPut]
        public async Task<IActionResult> Update(DispatchRequestList DispatchRequestList)
        {
            if (_DispatcherDataService.GetAsync(DispatchRequestList.WeekID) != null)
            {
                try
                {
                    var DispatchSolutions = DispatcherService.Shuffle(DispatchRequestList, DispatchRequestList.WeekID);
                    await _DispatcherDataService.CreateAsync(DispatchSolutions);
                    return Ok(DispatchSolutions);
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
            }
            else
            {
                return BadRequest("Weekly Scheduled Does not Exist");
            }
        }

        //Account Update
        [HttpDelete]
        public async Task<IActionResult> Delete(int WeekID)
        {
            if (_DispatcherDataService.GetAsync(WeekID) != null)
            {
                try
                {
                    await _DispatcherDataService.RemoveAsync(WeekID);
                    return Ok("Successfully Removed Weekly Task Mapping");
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
            }
            else
            {
                return BadRequest("Weekly Scheduled Does not Exist");
            }
        }
    }
}


//DispatchRequestList dispatchRequestList = new DispatchRequestList
//{
//    dpList = new List<DispatchRequest>
//            {
//                new DispatchRequest { EmpID = 1, CatAvailableSlots = new int[] { 90, 80, 75, 70 } },
//                new DispatchRequest { EmpID = 2, CatAvailableSlots = new int[] { 35, 85, 55, 65 } },
//                new DispatchRequest { EmpID = 3, CatAvailableSlots = new int[] { 125, 95, 90, 95 } },
//                new DispatchRequest { EmpID = 4, CatAvailableSlots = new int[] { 45, 110, 95, 115 } },
//                new DispatchRequest { EmpID = 5, CatAvailableSlots = new int[] { 50, 100, 90, 100 } },
//            },
//    MaxCat = 4
//};