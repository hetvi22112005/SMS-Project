using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class ExamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            /* var exam = _context.Exams
             .Include(s => s.Results)
             .Include(s => s.Course)
             .ToList();

             return View(exam); */

            var examsQuery = _context.Exams
           .Include(e => e.Course)
           .AsQueryable();

            // STUDENT: only published exams
            if (User.IsInRole("Student"))
            {
                examsQuery = examsQuery.Where(e => e.IsPublished);
            }

            // ADMIN & TEACHER: all exams
            var exams = await examsQuery
                .OrderByDescending(e => e.ExamDate)
                .ToListAsync();

            return View(exams);
        }
        public async Task<IActionResult> Details(int id)
        {
            var exam = await _context.Exams
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.ExamId == id);

            if (exam == null)
            {
                return NotFound();
            }

            // STUDENT SAFETY: cannot see unpublished exam details
            //    if (User.IsInRole("Student") && !exam.IsPublished)
            //  {
            //    return Forbid();
            //}

            return View(exam);
        }
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                exam.CreatedAt = DateTime.Now;
                _context.Exams.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = _context.Courses.ToList();
            return View(exam);
        }

        // ============================
        // EDIT (ADMIN / TEACHER)
        // ============================
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            ViewBag.Courses = _context.Courses.ToList();
            return View(exam);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exam exam)
        {
            if (id != exam.ExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                exam.UpdatedAt = DateTime.Now;
                _context.Update(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = _context.Courses.ToList();
            return View(exam);
        }
    }
}
