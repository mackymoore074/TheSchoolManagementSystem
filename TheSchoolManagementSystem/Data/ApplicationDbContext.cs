﻿using Microsoft.EntityFrameworkCore;
using TheSchoolManagementSystem.Models;

namespace TheSchoolManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the many-to-many relationship
            modelBuilder.Entity<StudentTeacher>()
                .HasKey(st => new { st.StudentId, st.TeacherId });

            modelBuilder.Entity<StudentTeacher>()
                .HasOne(st => st.Student)
                .WithMany(s => s.StudentTeachers)
                .HasForeignKey(st => st.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<StudentTeacher>()
                .HasOne(st => st.Teacher)
                .WithMany(t => t.StudentTeachers)
                .HasForeignKey(st => st.TeacherId)
                .OnDelete(DeleteBehavior.Cascade); // Allow cascade delete

            // Seed Administrator data
            modelBuilder.Entity<Administrator>().HasData(
                new Administrator
                {
                    AdministratorId = 2,
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "admin@example.com",
                    Password = "Admin@123"  // For actual apps, always hash the password!
                }
            );
        }
    }
}
