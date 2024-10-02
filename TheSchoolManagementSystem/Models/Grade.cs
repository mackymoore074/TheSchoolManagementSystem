using System.ComponentModel.DataAnnotations;

namespace TheSchoolManagementSystem.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [Required]
         [StringLength(255)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        public int? Marks { get; set; }

        // Foreign key for Student
        public ICollection<Student> Students { get; set; }
        // Navigation property - Admin can manage multiple teachers
        public ICollection<Teacher> Teachers { get; set; }

        public Grade()
        {
            Students = new List<Student>();
            Teachers = new List<Teacher>();
        }
    }

}
