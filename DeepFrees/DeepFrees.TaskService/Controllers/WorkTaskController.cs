using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.TaskService.Controllers
{
    public class WorkTaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
