using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public int UserId { get; set; }
        public int CourseId { get; set; }

        [Required, StringLength(20)]
        public string EnrollmentNo { get; set; }

        public DateTime DOB { get; set; }

        [StringLength(500)]
        public string Photo { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; }
        public Course Course { get; set; }

        public List<Result> Results { get; set; }
    }
}
