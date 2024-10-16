using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TheSchoolManagementSystem.Models
{
    public class Student
    {
        [Required]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Class Name is required")]
        public string ClassName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)] 
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Parent Name is required")]
        public string ParentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Parent Phone is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string ParentPhone { get; set; } = string.Empty;


        // Foreign key for administrator
        public int AdministratorId { get; set; } = 1;
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
