using Microsoft.AspNetCore.Mvc;
using DeepFrees.Scheduler.Model;
using DeepFrees.Scheduler.MicroService;

namespace DeepFrees.Scheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchedulingController : Controller
    {
        private readonly WorkTaskScheduler _WorkTaskScheduler;

        public SchedulingController(WorkTaskScheduler workTaskScheduler)
        {
            _WorkTaskScheduler = workTaskScheduler;
        }

        [HttpPost]
        public IActionResult ShuffleSchedule(List<Job> JobListing)
        {
            try
            {
                return Ok(WorkTaskScheduler.Shuffle(JobListing));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
