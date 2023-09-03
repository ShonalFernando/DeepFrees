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
        [HttpGet("{Username}")]
        public async Task<IActionResult> Get(int TaskID)
        {
            var uacc = await _DispatcherDataService.GetAsync(TaskID);

            if (uacc is null)
            {
                return NotFound();
            }

            return Ok(uacc);
        }

        //Account Creation
        [HttpPost]
        public async Task<IActionResult> Post(DispatchSolution DispatchSolution)
        {
            try
            {
                if (await _DispatcherDataService.GetAsync(DispatchSolution.TaskID) != null) 
                {
                    return BadRequest("Task Already Allocated");
                }
                else
                {
                    await _DispatcherDataService.CreateAsync(DispatchSolution);
                    return Created(nameof(Get), DispatchSolution);
                }

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        //Account Update
        [HttpPut]
        public async Task<IActionResult> Update(DispatchSolution DispatchSolution)
        {
            try
            {
                if (await _DispatcherDataService.GetAsync(DispatchSolution.TaskID) != null)
                {
                    await _DispatcherDataService.UpdateAsync(DispatchSolution.TaskID, DispatchSolution);

                    try
                    {
                        return Ok("Task Allocation Updated Succesfully");
                    }
                    catch (Exception e)
                    {

                        return BadRequest(e);
                    }
                }
                else
                {
                    return BadRequest("Task Allocation Does not Exist");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Account Update
        [HttpDelete]
        public async Task<IActionResult> Delete(DispatchSolution DispatchSolution)
        {
            var uacc = await _DispatcherDataService.GetAsync(DispatchSolution.TaskID);

            if (uacc is null)
            {
                return NotFound();
            }
            else
            {
                await _DispatcherDataService.RemoveAsync(DispatchSolution.TaskID);
                return Ok("Task Allocation Removed");
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