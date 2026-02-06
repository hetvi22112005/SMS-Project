using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Filter;

namespace StudentManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Courses.Where(c => c.IsActive).ToList());
        }
        public IActionResult Details()
        {
            return View(_context.Courses.ToList());
        }
        [HttpPost]
       
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
                return RedirectToAction("Index");

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
