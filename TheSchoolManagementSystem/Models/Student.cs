using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TheSchoolManagementSystem.Models
{
    public class Student
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ClassName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ParentName { get; set; }
        [Required]
        public string ParentPhone { get; set; }

        // Foreign key for administrator
        public int AdministratorId { get; set; }
        public Administrator Administrator { get; set; }

        // Grades and performance can be stored in a separate entity/table
        public ICollection<Grade> Grades { get; set; }
        // Navigation property - Student can have multiple teachers
        public ICollection<StudentTeacher> StudentTeachers { get; set; }
        public Student()
        {
            StudentTeachers = new List<StudentTeacher>();
        }

    }
}
