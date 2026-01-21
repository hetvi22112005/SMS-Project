using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required, StringLength(100)]
        public string CourseName { get; set; }

        public int Duration { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public List<Student> Students { get; set; }
        public List<Exam> Exams { get; set; }
    }
}
