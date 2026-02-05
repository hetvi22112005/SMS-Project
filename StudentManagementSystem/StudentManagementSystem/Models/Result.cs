using StudentManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public decimal MarksObtained { get; set; }
        public decimal MaxMarks { get; set; }

        public decimal? Percentage { get; set; }
        public string? Grade { get; set; }

        public bool IsPass { get; set; }

        public string? Remarks { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
