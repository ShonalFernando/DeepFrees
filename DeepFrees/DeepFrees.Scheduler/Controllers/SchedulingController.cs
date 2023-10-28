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
        private readonly WorkTaskScheduler _WorkTaskScheduler;
        private readonly JobDataService _JobDataService;

        public SchedulingController(JobDataService jobDataService,WorkTaskScheduler workTaskScheduler,DataService dataService)
        {
            _JobDataService = jobDataService;
            _DataService = dataService;
            _WorkTaskScheduler = workTaskScheduler;
        }


        //Job Creation
        [HttpPost]
        public async Task<IActionResult> JobPost(JobScheduleRequest JobRequestTable)
        {
            JobScheduleRequest jobSchedule = new();

            foreach (var jobListings in JobRequestTable.AllJobs)
            {
                foreach(var )
                var job = new Job();

                foreach (var task in jobTasks)
                {
                    job.Tasks.Add(new JobTask { team = task.team, duration = task.duration });
                }

                jobSchedule.AllJobs.Add(job);
            }
            _WorkTaskScheduler.Shuffle(jobSchedule);
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
