using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentManagementSystem.Filter
{
    public class RoleAthorizeAttribute : ActionFilterAttribute
    {
        private readonly string _role;

        public RoleAthorizeAttribute(string role)
        {
            _role = role;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");

            if (role == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            // Role hierarchy
            if (_role == "Student" && role != "Student" && role != "Teacher" && role != "Admin")
                context.Result = new RedirectToActionResult("Login", "Account", null);

            if (_role == "Teacher" && role != "Teacher" && role != "Admin")
                context.Result = new RedirectToActionResult("Login", "Account", null);

            if (_role == "Admin" && role != "Admin")
                context.Result = new RedirectToActionResult("Login", "Account", null);

        }
    }
}
