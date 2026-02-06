namespace StudentManagementSystem.Models.ViewModels
{
    public class StudentDashboardVM
    {
        public string FullName { get; set; }
        public string EnrollmentNo { get; set; }
        public string CourseName { get; set; }
        public int Semester { get; set; }

        public int TotalExams { get; set; }
        public int PublishedResults { get; set; }
        public int PassedExams { get; set; }
        public int FailedExams { get; set; }

        public string Photo { get; set; }
    }
}
