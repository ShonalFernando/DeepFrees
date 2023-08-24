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

        [HttpGet]
        public IActionResult ScheduleTasks()
        {

            try
            {
                _WorkTaskScheduler.Schedule(new string[] { "asas", "asasas" });
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        public class SchedulerInput
        {
            public List<EmployeeSlot> Employees { get; set; }
            public List<WorkTask> Tasks { get; set; }
        }
    }
}
