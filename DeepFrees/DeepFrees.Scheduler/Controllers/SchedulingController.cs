using Microsoft.AspNetCore.Mvc;
using DeepFrees.Scheduler.Model;
using DeepFrees.Scheduler.MicroService;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DeepFrees.Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingController : ControllerBase
    {
        private readonly DataService _DataService;
        private readonly WorkTaskScheduler _WorkTaskScheduler;
        private readonly JobDataService _JobDataService;

        public SchedulingController(JobDataService jobDataService,WorkTaskScheduler workTaskScheduler,DataService dataService)
        {
            _JobDataService = jobDataService;
            _DataService = dataService;
            _WorkTaskScheduler = workTaskScheduler;
        }

        [HttpGet("GetTasks")]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await _JobDataService.GetAsync());
        }


        //Job Creation
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(JobTask JobTask)
        {
            JobTask._id =ObjectId.GenerateNewId();
            await _JobDataService.CreateAsync(JobTask);
            return Ok();
        }

        //Job Creation
        [HttpPost("Shuffle")]
        public async Task<IActionResult> Shuffle()
        {
            JobScheduleRequest jobSchedule = new();

            var JobsListing = await _JobDataService.GetAsync();

            var groupedTasks = JobsListing
                .GroupBy(task => task.weekID)
                .ToDictionary(group => group.Key, group => group.ToList());

            var jobSchedule2 = new JobScheduleRequest();

            foreach (var weekId in groupedTasks.Keys)
            {
                var job = new Job();
                job.Tasks.AddRange(groupedTasks[weekId]);
                jobSchedule.AllJobs.Add(job);
            }

            _WorkTaskScheduler.Shuffle(jobSchedule2);
            return Ok();
        }

        //Job Creation
        [HttpPost("Tester")]
        public async Task<IActionResult> Shuffle(JobScheduleRequest JobRequestTable)
        {

            _WorkTaskScheduler.Shuffle(JobRequestTable);
            return Ok();
        }
        ////Account Update
        //[HttpPut]
        //public async Task<IActionResult> Update(WeeklyJob WeeklyJob)
        //{
        //    if (_DataService.GetAsync(WeeklyJob.WeekID) != null)
        //    {
        //        try
        //        {
        //            var DispatchSolutions = WorkTaskScheduler.Shuffle(WeeklyJob);
        //            await _DataService.CreateAsync(DispatchSolutions);
        //            return Ok(DispatchSolutions);
        //        }
        //        catch (Exception e)
        //        {
        //            return Problem(e.Message);
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Weekly Schedule Does not Exist");
        //    }
        //}

        ////Account Update
        //[HttpDelete]
        //public async Task<IActionResult> Delete(int WeekID)
        //{
        //    if (_DataService.GetAsync(WeekID) != null)
        //    {
        //        try
        //        {
        //            await _DataService.RemoveAsync(WeekID);
        //            return Ok("Successfully Removed Weekly Task Mapping");
        //        }
        //        catch (Exception e)
        //        {
        //            return Problem(e.Message);
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Weekly Scheduled Does not Exist");
        //    }
        //}
    }
}
