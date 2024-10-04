using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSchoolManagementSystem.Data;
using TheSchoolManagementSystem.Models;
using System.Linq;

namespace TheSchoolManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teacher/Authenticate
        public IActionResult Authenticate()
        {
            return View();
        }

        // POST: Teacher/Authenticate
        [HttpPost]
        public IActionResult Authenticate(int teacherId)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);

            if (teacher == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Teacher ID.");
                return View(); // Re-display the view with an error message
            }

            // Redirect to the Index action with the authenticated teacher's ID
            return RedirectToAction("Index", new { id = teacherId });
        }

        // GET: Teacher Index (Dashboard)
        public IActionResult Index(int id)
        {
            var teacher = _context.Teachers
                .Include(t => t.StudentTeachers) // Include any related data if necessary
                .FirstOrDefault(t => t.TeacherId == id); // Find teacher by ID

            if (teacher == null)
            {
                return NotFound(); // Return a 404 if the teacher does not exist
            }

            var subject = _context.Subjects
                .FirstOrDefault(s => s.TeacherId == id);
            Console.WriteLine($"Error occurred: {subject}");
            // ViewBag.SubjectId = subject.subjectId;

            ViewBag.SubjectId = subject?.SubjectId;
            return View(teacher); // Pass the teacher object to the view
        }

        // GET: Teacher Profile
        public IActionResult Profile(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // GET: View Students in a Subject
        public IActionResult ViewStudents(int teacherId, int subjectId)
        {
            var students = _context.Registrations
                .Include(r => r.Student)
                .Where(r => r.SubjectId == subjectId)
                .ToList();

            ViewBag.SubjectId = subjectId;
            return View(students);
        }

        // GET: Award Marks to a Student
        public IActionResult AwardMarks(int registrationId)
        {
            var registration = _context.Registrations.FirstOrDefault(r => r.RegistrationId == registrationId);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Award Marks to a Student
        [HttpPost]
        public IActionResult AwardMarks(int registrationId, int marks)
        {
            var registration = _context.Registrations
        .Include(r => r.Student) 
        .FirstOrDefault(r => r.RegistrationId == registrationId);

            if (registration == null)
            {
                return NotFound();
            }

            registration.Marks = marks;
            registration.Grade = registration.GetLetterGrade();
            _context.SaveChanges();
            return View(registration);
        }
    }
}
