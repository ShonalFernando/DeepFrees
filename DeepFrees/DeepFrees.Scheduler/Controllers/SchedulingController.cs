using Microsoft.AspNetCore.Mvc;
using DeepFrees.Scheduler.Model;
using DeepFrees.Scheduler.MicroService;
using System.Threading.Tasks;

namespace DeepFrees.Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingController : ControllerBase
    {
        private readonly DataService _DataService;

        public SchedulingController(DataService dataService)
        {
            _DataService = dataService;
        }

        //Get UserDetails
        [HttpGet("{WeekID}")]
        public async Task<IActionResult> Get(int WeekID)
        {
            var uacc = await _DataService.GetAsync(WeekID);

            if (uacc is null)
            {
                return NotFound();
            }

            return Ok(uacc);
        }

        //Account Creation
        [HttpPost]
        public async Task<IActionResult> Post(WeeklyJob WeeklyJob)
        {
            if (_DataService.GetAsync(WeeklyJob.WeekID) == null)
            {
                try
                {
                    var DispatchSolutions = WorkTaskScheduler.Shuffle(WeeklyJob);
                    await _DataService.CreateAsync(DispatchSolutions);
                    return Ok(DispatchSolutions);
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
            }
            else
            {
                return BadRequest("Weekly Employee Task Scheduled Already Exist");
            }
        }

        //Account Update
        [HttpPut]
        public async Task<IActionResult> Update(WeeklyJob WeeklyJob)
        {
            if (_DataService.GetAsync(WeeklyJob.WeekID) != null)
            {
                try
                {
                    var DispatchSolutions = WorkTaskScheduler.Shuffle(WeeklyJob);
                    await _DataService.CreateAsync(DispatchSolutions);
                    return Ok(DispatchSolutions);
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
            }
            else
            {
                return BadRequest("Weekly Schedule Does not Exist");
            }
        }

        //Account Update
        [HttpDelete]
        public async Task<IActionResult> Delete(int WeekID)
        {
            if (_DataService.GetAsync(WeekID) != null)
            {
                try
                {
                    await _DataService.RemoveAsync(WeekID);
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
