using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var teachers = _context.Teachers
                .Include(t => t.User)
                .ToList();

            return View(teachers);
        }

        public IActionResult Details()
        {
            var teachers = _context.Teachers
                .Include(t => t.User)
                .ToList();

            return View(teachers);
        }
    }
}
