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
        private readonly ExampleService _ExampleService;

        public DispatcherController(ExampleService exampleService, TaskAssigner taskAssigner, TaskDataService taskDataService, TechnicianDataService technicianDataService, TaskTransformer taskTransformer, DispatcherDataService dispatcherDataService, DispatcherService dispatcherService)
        {
            _DispatcherDataService = dispatcherDataService;
            _DispatcherService = dispatcherService;
            _TaskTransformer = taskTransformer;
            _TechnicianDataService = technicianDataService;
            _TaskDataService = taskDataService;
            _TaskAssigner = taskAssigner;
            _ExampleService = exampleService;
        }

        //www.deepfrees.lk
        //www.deepfrees.net/Api/TaskAssigner/Shuffle
        //Shuffling through Post

        [HttpPost("ShuffleTest")]
        public async Task<IActionResult> ShuffleSample(List<DispatchRequest> dpList)
        {
            var TaskArrays = _TaskTransformer.TransformTasks(dpList);
            var UnformattedSolutions = _DispatcherService.Shuffle(TaskArrays, dpList); //Solver

            //As the Database is updated it is okay to return the unformatted solution, the front end will manage it
            return Ok(UnformattedSolutions);
        }


        [HttpGet("Shuffle")]
        public async Task<IActionResult> Get()
        {
            List<DispatchRequest> DispatchRequestList = new();
            var Technicians = await _TechnicianDataService.GetAsync();

            foreach(var tex in Technicians)
            {
                if (tex.WorkTaskPointTable != null)
                {
                    foreach (var dtp in tex.WorkTaskPointTable)
                    {
                        DispatchRequest _DispatchRequest = new();
                        _DispatchRequest.TaskCategoryID = dtp.TaskCategory;
                        _DispatchRequest.EmployeeID = tex.NIC;
                        _DispatchRequest.TaskPoints = dtp.TaskCategoryPoints;
                        DispatchRequestList.Add(_DispatchRequest);
                    } 
                }
            }
            foreach(var req in DispatchRequestList)
            {
                await Console.Out.WriteLineAsync(req.EmployeeID + " : " + req.TaskCategoryID + " : " + req.TaskPoints);
            }

            var TaskArrays = _TaskTransformer.TransformTasks(DispatchRequestList);
            var UnformattedSolutions = _DispatcherService.Shuffle(TaskArrays, DispatchRequestList); //Solver
            var texhs = await _TechnicianDataService.GetAsync();
            var workx = await _TaskDataService.GetAsync();

            var solvs = _TaskAssigner.AssignTasks(UnformattedSolutions, workx, texhs); //THis is used to Assign tasks based on solvers recommendations (Output is Unformated)

            foreach(var techs in solvs.Item2)
            {
                try
                {
                    await _TechnicianDataService.UpdateAsync(techs.NIC, techs);
                }
                catch (Exception e)
                {

                    await Console.Out.WriteLineAsync(e.Message); ;
                }
            }

            foreach (var wtasks in solvs.Item1)
            {
                 await _TaskDataService.UpdateAsync(wtasks.taskID, wtasks);
            }

            //As the Database is updated it is okay to return the unformatted solution, the front end will manage it
            return Ok(solvs);
        }

    }
}
