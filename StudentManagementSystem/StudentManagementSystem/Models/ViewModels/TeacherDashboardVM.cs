namespace StudentManagementSystem.Models.ViewModels
{
    public class TeacherDashboardVM
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Qualification { get; set; }
        public int Experience { get; set; }
        public string Photo { get; set; }

        public int TotalExams { get; set; }
        public int ResultsEntered { get; set; }
        public int PublishedExams { get; set; }
        public int StudentsEvaluated { get; set; }
    }
}
