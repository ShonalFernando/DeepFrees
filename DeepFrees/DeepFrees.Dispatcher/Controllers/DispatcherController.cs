using DeepFrees.Dispatcher.Microservice;
using DeepFrees.Dispatcher.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DeepFrees.Dispatcher.Controllers
{
    [Route("DispatchSolver/[controller]")]
    [ApiController]
    public class DispatcherController : ControllerBase
    {
        private readonly DispatcherDataService _DispatcherDataService;
        private readonly DispatcherService _DispatcherService;
        private readonly TaskTransformer _TaskTransformer;
        private readonly TechnicianDataService _TechnicianDataService;
        private readonly TaskDataService _TaskDataService; 
        private readonly TaskAssigner _TaskAssigner;

        public DispatcherController(TaskAssigner taskAssigner, TaskDataService taskDataService, TechnicianDataService technicianDataService, TaskTransformer taskTransformer, DispatcherDataService dispatcherDataService, DispatcherService dispatcherService)
        {
            _DispatcherDataService = dispatcherDataService;
            _DispatcherService = dispatcherService;
            _TaskTransformer = taskTransformer;
            _TechnicianDataService = technicianDataService;
            _TaskDataService = taskDataService;
            _TaskAssigner = taskAssigner;
        }


        //Shuffling through Post
        [HttpPost("Shuffle")]
        public async Task<IActionResult> Post([FromBody] List<DispatchRequest> DispatchRequestList)
        {


            var TaskArrays = _TaskTransformer.TransformTasks(DispatchRequestList);
            var UnformattedSolutions = _DispatcherService.Shuffle(TaskArrays, DispatchRequestList);
            var texhs = await _TechnicianDataService.GetAsync();
            var workx = await _TaskDataService.GetAsync();

            var solvs = _TaskAssigner.AssignTasks(UnformattedSolutions, workx, texhs);

            foreach(var techs in solvs.Item2)
            {
                await _TechnicianDataService.UpdateAsync(techs.NIC, techs);
            }

            foreach (var wtasks in solvs.Item1)
            {
                await _TaskDataService.UpdateAsync(wtasks.taskID, wtasks);
            }

            return Ok();
        }

    }
}
