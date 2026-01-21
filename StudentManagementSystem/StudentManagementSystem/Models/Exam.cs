using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required, StringLength(100)]
        public string ExamName { get; set; }

        public int CourseId { get; set; }

        public DateTime ExamDate { get; set; }

        public int MaxMarks { get; set; }
        public int ObtainedMarks { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Course Course { get; set; }
        public List<Result> Results { get; set; }
    }
}
