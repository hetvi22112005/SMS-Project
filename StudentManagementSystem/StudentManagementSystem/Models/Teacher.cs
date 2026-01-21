using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        public int UserId { get; set; }

        [StringLength(100)]
        public string Qualification { get; set; }

        public int Experience { get; set; }

        [StringLength(500)]
        public string Photo { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; }

        public List<Result> ResultsCreated { get; set; }
    }
}
