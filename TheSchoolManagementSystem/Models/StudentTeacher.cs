namespace TheSchoolManagementSystem.Models
{
    public class StudentTeacher
    {
        public int StudentId { get; set; }=1;
        public Student? Student { get; set; }

        public int TeacherId { get; set; }=1;
        public Teacher? Teacher { get; set; }
    }

}
