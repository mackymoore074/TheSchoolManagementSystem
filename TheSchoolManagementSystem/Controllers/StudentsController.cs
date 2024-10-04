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
            var student = _context.Students
                .Include(s => s.StudentTeachers) // Include related data if necessary
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
        // GET: Register for Subjects
        public IActionResult RegisterSubject(int id)
        {
            var student = _context.Students.Find(id); // Use Find for better readability
            if (student == null)
            {
                return NotFound();
            }

            // Get subjects that the student has not registered for
            var registeredSubjectIds = _context.Registrations
                .Where(r => r.StudentId == id)
                .Select(r => r.SubjectId)
                .ToList();

            // List subjects the student hasn't registered for yet
            var availableSubjects = _context.Subjects
                .Where(s => !registeredSubjectIds.Contains(s.SubjectId))
                .ToList();

            ViewBag.StudentId = id;

            // Pass only available subjects to the view
            return View(availableSubjects);
        }


        // POST: Register for Subject
        [HttpPost]
        // POST: Register for Subject
        [HttpPost]
        public IActionResult RegisterSubject(int studentId, int[] selectedSubjectIds)
        {
            foreach (var subjectId in selectedSubjectIds)
            {
                // Check if the student is already registered for the subject
                if (_context.Registrations.Any(r => r.StudentId == studentId && r.SubjectId == subjectId))
                {
                    ModelState.AddModelError(string.Empty, $"You are already registered for subject with ID {subjectId}.");
                    continue; // Skip to the next subject if already registered
                }

                // Register the student for the subject
                var registration = new Registration
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    Marks = 0 // Initial marks can be set to 0
                };

                // Calculate grade directly in the controller
                registration.Grade = CalculateGrade(registration.Marks);

                _context.Registrations.Add(registration);
            }

            // Save changes to the database after processing all subjects
            _context.SaveChanges();

            // Redirect to view grades after successful registration
            return RedirectToAction("ViewGrades", new { id = studentId });
        }

        // Utility method to calculate the grade based on Marks
        private string CalculateGrade(int? marks)
        {
            if (marks == null)
            {
                return "No Marks"; // Handle case where marks are not set
            }
            if (marks >= 90 && marks <= 100)
            {
                return "A";
            }
            else if (marks >= 75 && marks <= 89)
            {
                return "B";
            }
            else if (marks >= 65 && marks <= 74)
            {
                return "C";
            }
            else if (marks >= 50 && marks <= 64)
            {
                return "D";
            }
            else
            {
                return "F";
            }
        }



        // GET: View Grades
        public IActionResult ViewGrades(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            // Get all the grades for the student from the registration table
            var grades = _context.Registrations
                                 .Where(r => r.StudentId == id)
                                 .Include(r => r.Subject)
                                 .ToList();

            if (!grades.Any())
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
