namespace StudentManagementSystem.Models.ViewModels
{
    public class AdminDashboardVM
    {
        public int TotalStudents { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalCourses { get; set; }
        public int ActiveUsers { get; set; }
        public int PublishedExams { get; set; }
        public int PendingResults { get; set; }
    }
}
