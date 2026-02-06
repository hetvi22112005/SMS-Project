using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.ViewModels;
namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // LOGIN PAGE
        [HttpGet]
        public IActionResult Login()
        {
            if (Request.Cookies.ContainsKey("RememberEmail"))
            {
                ViewBag.RememberedEmail = Request.Cookies["RememberEmail"];
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, bool RememberMe)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u =>
                u.Email == model.Email &&
                u.Password == model.Password &&
                u.IsActive == true);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View(model);
            }

            // SESSION
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.FullName);

            // 👉 REMEMBER ME COOKIE
            if (RememberMe)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append("RememberEmail", user.Email, options);
            }
            else
            {
                Response.Cookies.Delete("RememberEmail");
            }

            // Redirect by role
            if (user.Role == "Admin")
                return RedirectToAction("AdminDashboard", "Admin");

            if (user.Role == "Teacher")
                return RedirectToAction("Dashboard", "Teachers");

            return RedirectToAction("Dashboard", "Students");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if email already exists
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Email == model.Email);

            if (existingUser != null)
            {
                ViewBag.Error = "Email already registered";
                return View(model);
            }

            // Create new student user
            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password, // plain for now
                Role = "Student",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                ViewBag.Message = "Email not found";
                return View();
            }

            // SIMPLE PROJECT LOGIC
            ViewBag.Message = $"Your password is: {user.Password}";

            return View();
        }

    }
}
