using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
