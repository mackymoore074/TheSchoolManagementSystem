namespace TheSchoolManagementSystem.Models
{
    public class StudentTeacher
    {
        public int StudentId { get; set; } 
        public Student? Student { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }

}
