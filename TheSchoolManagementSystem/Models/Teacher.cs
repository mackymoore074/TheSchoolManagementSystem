using System.ComponentModel.DataAnnotations;

namespace TheSchoolManagementSystem.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        // Foreign key for Administrator
        public int AdministratorId { get; set; } = 1;
        public Administrator? Administrator { get; set; }
        // Navigation property
        public ICollection<StudentTeacher> StudentTeachers { get; set; }

        public Teacher()
        {
            StudentTeachers = new List<StudentTeacher>();
        }


    }
}
