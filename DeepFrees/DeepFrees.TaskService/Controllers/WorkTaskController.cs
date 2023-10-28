using DeepFrees.TaskService.MicroService;
using DeepFrees.TaskService.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Security.Cryptography;

namespace DeepFrees.TaskService.Controllers
{
    [Route("TaskService/[controller]")]
    [ApiController]
    public class WorkTaskController : ControllerBase
    {
        private readonly TaskDataContext _WorkTaskDataContext;

        public WorkTaskController(TaskDataContext taskDataContext)
        {
            _WorkTaskDataContext = taskDataContext;
        }

        [HttpGet("GetTasks")]
        public async Task<IActionResult> Get() //This method returns a List of Tasks
        {
            var WorkTasks = await _WorkTaskDataContext.GetAsync();

            if (WorkTasks != null)
            {
                return Ok(WorkTasks);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetTasks/{TaskID}")]
        public async Task<IActionResult> Get([FromRoute] ObjectId TaskID) //This method returns a single Task
        {
            var WorkTask1 = await _WorkTaskDataContext.GetAsync(TaskID);

            if (WorkTask1 != null)
            {
                return Ok(WorkTask1);
            }
            else
            {
                return NotFound();
            }
        }

        //Tasks Creation
        [HttpPost("CreateTask")]
        public async Task<IActionResult> Post(WorkTask WorkTask) //Create a Single Task

        {
            if (WorkTask != null)
            {
                WorkTask._id = ObjectId.GenerateNewId();
                try
                {
                    await _WorkTaskDataContext.CreateAsync(WorkTask);
                    await Console.Out.WriteLineAsync("Test 3.4");
                    return Ok();
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync("Test 3.5");
                    await Console.Out.WriteLineAsync(ex.Message);
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Test 3.6");

                return BadRequest();
            }
        }

        //Tasks Update
        [HttpPut("UpdateTask/{TaskID}")]
        public async Task<IActionResult> Update([FromRoute] ObjectId TaskID, [FromBody] WorkTask WorkTask)
        {
            if (WorkTask != null)
            {
                await _WorkTaskDataContext.UpdateAsync(TaskID, WorkTask);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //Tasks Delete
        [HttpDelete("DeleteTask/{TaskID}")]
        public async Task<IActionResult> Delete(ObjectId TaskID)
        {
            var WorkTask1 = await _WorkTaskDataContext.GetAsync(TaskID);

            if (WorkTask1 != null)
            {
                await _WorkTaskDataContext.RemoveAsync(TaskID);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
