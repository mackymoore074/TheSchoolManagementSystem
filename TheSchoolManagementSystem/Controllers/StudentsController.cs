using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSchoolManagementSystem.Data;
using TheSchoolManagementSystem.Models;
using System.Linq;

namespace TheSchoolManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Students/Authenticate
        public IActionResult Authenticate()
        {
            return View();
        }

        // POST: Students/Authenticate
        [HttpPost]
        public IActionResult Authenticate(int studentId)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

            if (student == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Student ID.");
                return View(); // Re-display the view with an error message
            }

            // Redirect to the Index action with the authenticated student's ID
            return RedirectToAction("Index", new { id = studentId });
        }
       
        // GET: Student Dashboard (Index)
        public IActionResult Index(int id)
        {
            // Fetch the student from the database using the provided StudentId
            var student = _context.Students
                .Include(s => s.StudentTeachers) // Include any related data if necessary
                .FirstOrDefault(s => s.StudentId == id); // Find student by ID

            if (student == null)
            {
                return NotFound(); // Return a 404 if the student does not exist
            }

            return View(student); // Pass the student object to the view
        }


        // GET: View Student Profile
        public IActionResult Profile(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Register for Subjects
        public IActionResult RegisterSubject(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            // Get all available subjects (you can customize this to exclude already registered subjects)
            var subjects = _context.Subjects.ToList();
            ViewBag.StudentId = id;

            return View(subjects);
        }

        // POST: Register for Subject
        [HttpPost]
        public IActionResult RegisterSubject(int studentId, int subjectId)
        {
            // Check if the student is already registered for the subject
            var existingRegistration = _context.Registrations
                .FirstOrDefault(r => r.StudentId == studentId && r.SubjectId == subjectId);

            if (existingRegistration != null)
            {
                ModelState.AddModelError("", "You are already registered for this subject.");
                return RedirectToAction("RegisterSubject", new { id = studentId });
            }

            // Register the student for the subject
            var registration = new Registration
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Marks = 0 // Initial marks can be set to 0
            };

            _context.Registrations.Add(registration);
            _context.SaveChanges();

            return RedirectToAction("ViewGrades", new { id = studentId });
        }

        // GET: View Grades
        public IActionResult ViewGrades(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            // Get all the grades for the student from the registration table
            var grades = _context.Registrations
                                 .Where(r => r.StudentId == id)
                                 .Include(r => r.Subject)
                                 .ToList();

            if (grades.Count == 0)
            {
                ViewBag.NoGrades = "You have not been graded for any subjects yet.";
            }

            return View(grades);
        }

        // Utility method to check if a student exists by ID (optional but useful)
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
