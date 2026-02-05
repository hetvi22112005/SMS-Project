using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // if (HttpContext.Session.GetString("Role") == null)
            //   return RedirectToAction("Login", "Account");

            //return View(_context.Students.ToList());
            var students = _context.Students
            .Include(s => s.User)
            .Include(s => s.Course)
            .ToList();

            return View(students);

        }
        public IActionResult Details()
        {
            var student = _context.Students
                .Include(s => s.User)
                .Include(s => s.Course)
                .ToList();

            return View(student);
        }
    }
}
