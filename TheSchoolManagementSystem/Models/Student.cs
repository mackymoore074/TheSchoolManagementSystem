using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TheSchoolManagementSystem.Models
{
    public class Student
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }= string.Empty;
        [Required]
        public string LastName { get; set; }= string.Empty;
        [Required]
        public string ClassName { get; set; }= string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }= string.Empty;
        [Required]
        public string ParentName { get; set; }= string.Empty;
        [Required]
        public string ParentPhone { get; set; }= string.Empty;

        // Foreign key for administrator
        public int AdministratorId { get; set; }=1;
        public Administrator? Administrator { get; set; }

        // Grades and performance can be stored in a separate entity/table
        // Navigation property - Student can have multiple teachers
        public ICollection<StudentTeacher> StudentTeachers { get; set; }
        public Student()
        {
            StudentTeachers = new List<StudentTeacher>();
        }

    }
}
