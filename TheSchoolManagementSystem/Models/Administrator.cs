using System.ComponentModel.DataAnnotations;

namespace TheSchoolManagementSystem.Models
{
    public class Administrator
    {
        [Key]
        public int AdministratorId { get; set; }

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

        // Navigation property - Admin can manage multiple students
        public ICollection<Student> Students { get; set; }

        // Navigation property - Admin can manage multiple teachers
        public ICollection<Teacher> Teachers { get; set; }

        public Administrator()
        {
            Students = new List<Student>();
            Teachers = new List<Teacher>();
        }
    }

}
