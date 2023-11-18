using Microsoft.AspNetCore.Mvc;

namespace myTH01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/file-manager")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
