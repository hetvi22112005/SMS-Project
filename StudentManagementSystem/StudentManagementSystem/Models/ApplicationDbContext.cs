using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Result> Results { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =======================
        // USER
        // =======================
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);

            entity.Property(u => u.FullName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(u => u.Email)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(u => u.Password)
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(u => u.Role)
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(u => u.IsActive)
                  .HasDefaultValue(true);

            entity.Property(u => u.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");
        });

        // =======================
        // COURSE
        // =======================
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.CourseId);

            entity.Property(c => c.CourseName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(c => c.Description)
                  .HasMaxLength(500);

            entity.Property(c => c.Department)
                  .HasMaxLength(100);

            entity.Property(c => c.IsActive)
                  .HasDefaultValue(true);

            entity.Property(c => c.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");
        });

        // =======================
        // TEACHER
        // =======================
        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(t => t.TeacherId);

            entity.Property(t => t.Qualification)
                  .HasMaxLength(100);

            entity.Property(t => t.Bio)
                  .HasMaxLength(500);

            entity.Property(t => t.Department)
                  .HasMaxLength(100);

            entity.Property(t => t.Photo)
                  .HasMaxLength(1000);

            entity.Property(t => t.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(t => t.User)
                  .WithOne(u => u.Teacher)
                  .HasForeignKey<Teacher>(t => t.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // =======================
        // STUDENT
        // =======================
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.StudentId);

            entity.Property(s => s.EnrollmentNo)
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(s => s.Photo)
                  .HasMaxLength(1000);

            entity.Property(s => s.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(s => s.User)
                  .WithOne(u => u.Student)
                  .HasForeignKey<Student>(s => s.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Course)
                  .WithMany(c => c.Students)
                  .HasForeignKey(s => s.CourseId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // =======================
        // EXAM
        // =======================
        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId);

            entity.Property(e => e.ExamName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.ExamType)
                  .HasMaxLength(50);

            entity.Property(e => e.IsPublished)
                  .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Exam)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // =======================
        // RESULT
        // =======================
        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(r => r.ResultId);

            entity.Property(r => r.Grade)
                  .HasMaxLength(5);

            entity.Property(r => r.IsPass)
                  .HasMaxLength(10)
                  .IsRequired();

            entity.Property(r => r.IsActive)
                  .HasDefaultValue(true);

            entity.Property(r => r.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(r => r.Student)
                  .WithMany(s => s.Results)
                  .HasForeignKey(r => r.StudentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Exam)
                  .WithMany(e => e.Result)
                  .HasForeignKey(r => r.ExamId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Course)
                  .WithMany(c => c.Result)
                  .HasForeignKey(r => r.CourseId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Teacher)
                  .WithMany(t => t.Results)
                  .HasForeignKey(r => r.TeacherId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(r => r.IsPass)
                  .HasDefaultValue(true);

            entity.Property(r => r.MarksObtained)
                  .HasColumnType("decimal(5,2)");

            entity.Property(r => r.MaxMarks)
                  .HasColumnType("decimal(5,2)");

            entity.Property(r => r.Percentage)
                  .HasColumnType("decimal(5,2)");
        });
    }
}
