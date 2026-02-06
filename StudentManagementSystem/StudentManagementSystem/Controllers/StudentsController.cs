using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Filter;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
    {
    [RoleAthorizeAttribute("Student")]
    public class StudentsController : Controller
    {

        private readonly ApplicationDbContext _context;
       // private readonly IWebHostEnvironment _env;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var students = _context.Students
            .Include(s => s.User)
            .Include(s => s.Course)
            .ToList();

            return View(students);

        }
       public IActionResult Dashboard()
        {
            // Logged-in user id from session
            //int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdStr); 

            var student = _context.Students
                .Include(s => s.Course)
                .Include(s => s.User)
                .FirstOrDefault(s => s.UserId == userId);

            if (student == null)
                return NotFound();

            StudentDashboardVM vm = new StudentDashboardVM
            {
                FullName = student.User.FullName,
                EnrollmentNo = student.EnrollmentNo,
                CourseName = student.Course.CourseName,
                Semester = student.Semester,
                Photo = student.Photo,

                TotalExams = _context.Exams
                    .Where(e => e.CourseId == student.CourseId &&
                                e.Semester == student.Semester)
                    .Count(),

                PublishedResults = _context.Results
                    .Where(r => r.StudentId == student.StudentId &&
                                r.IsActive)
                    .Count(),

                PassedExams = _context.Results
                    .Where(r => r.StudentId == student.StudentId &&
                                r.IsPass)
                    .Count(),

                FailedExams = _context.Results
                    .Where(r => r.StudentId == student.StudentId &&
                                !r.IsPass)
                    .Count()
            };

            return View(vm);
        }   

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "admin")
                return Unauthorized();

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Details()
        {
            var student = _context.Students
                .Include(s => s.User)
                .Include(s => s.Course)
                .ToList();

            return View(student);
        }
    /*    public IActionResult Create()
        {
            ViewBag.Users = _context.Users.Where(u => u.Role == "Student").ToList();
            ViewBag.Courses = _context.Courses.Where(c => c.IsActive).ToList();
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student, IFormFile PhotoFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = _context.Users.Where(u => u.Role == "Student").ToList();
                ViewBag.Courses = _context.Courses.Where(c => c.IsActive).ToList();
                return View(student);
            }

            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(PhotoFile.FileName);
                var path = Path.Combine(uploads, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                PhotoFile.CopyTo(stream);

                student.Photo = "/uploads/" + fileName;
            }

            student.CreatedAt = DateTime.Now;
            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details));
        }  */

        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Student student, IFormFile PhotoFile)
        {
            try
            {
                if (PhotoFile != null && PhotoFile.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(PhotoFile.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        PhotoFile.CopyTo(stream);
                    }

                    student.Photo = fileName;
                }

                _context.Update(student);
                _context.SaveChanges();

                TempData["msg"] = "Successfully edited";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["msg"] = "Sorry! Try again";
                return RedirectToAction("Edit", new { id = student.StudentId });
            }
        }
    }
}
