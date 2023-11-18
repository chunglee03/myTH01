using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myTH01.Models;

namespace myTH01.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DataContext _context;
        public PostController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            var mnList = (from m in _context.Menus
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString()
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
