using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required, StringLength(100)]
        public string CourseName { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }

        public int Duration { get; set; }

        [StringLength(100)]
        public string Department {  get; set; }
        public int TotalCredits { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public List<Student> Students { get; set; }
        public List<Exam> Exams { get; set; }
    }
}
