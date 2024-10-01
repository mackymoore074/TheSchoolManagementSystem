using System.ComponentModel.DataAnnotations;

namespace TheSchoolManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Subject { get; set; }
        // Foreign key for Administrator
        public int AdministratorId { get; set; }
        public Administrator Administrator { get; set; }
        // Navigation property
        public ICollection<StudentTeacher> StudentTeachers { get; set; }

        public Teacher()
        {
            StudentTeachers = new List<StudentTeacher>();
        }


    }
}
