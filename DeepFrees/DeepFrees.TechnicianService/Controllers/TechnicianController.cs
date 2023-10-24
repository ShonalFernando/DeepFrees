using Microsoft.AspNetCore.Mvc;

namespace DeepFrees.TechnicianService.Controllers
{
    public class TechnicianController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
