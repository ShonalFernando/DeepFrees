using Microsoft.AspNetCore.Mvc;
using DeepFrees.Scheduler.Model;
using DeepFrees.Scheduler.MicroService;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeepFrees.Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingController : ControllerBase
    {
        private readonly DataService _DataService;
        private readonly WorkTaskScheduler _WorkTaskScheduler;
        private readonly JobDataService _JobDataService;

        public SchedulingController(JobDataService jobDataService, WorkTaskScheduler workTaskScheduler, DataService dataService)
        {
            _JobDataService = jobDataService;
            _DataService = dataService;
            _WorkTaskScheduler = workTaskScheduler;
        }


        [HttpGet("GetSchedules")]
        public async Task<IActionResult> GetSchedules()
        {

            var req = await _JobDataService.GetAsync();
            List<AssignedJobs> ajobs = _WorkTaskScheduler.Shuffle(req);

            foreach (var ajbs in ajobs)
            {
                ajbs._id = ObjectId.GenerateNewId();
                await _DataService.CreateAsync(ajbs);
            }
            return Ok();
        }

        [HttpGet("GetTasks")]
        public async Task<IActionResult> GetTasks()
        {

            var req = await _JobDataService.GetAsync();
            return Ok(req);
        }

        //Job Creation
        [HttpPost("AddNewWeekJobs")]
        public async Task<IActionResult> AddNewWeekJobs(JobCollection JobCollection)
        {
            JobCollection._id = ObjectId.GenerateNewId();
            await _JobDataService.CreateAsync(JobCollection);
            return Ok();
        }

    }
}
