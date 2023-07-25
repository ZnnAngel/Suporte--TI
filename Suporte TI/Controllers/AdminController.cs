using Microsoft.AspNetCore.Mvc;

namespace Suporte_TI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
