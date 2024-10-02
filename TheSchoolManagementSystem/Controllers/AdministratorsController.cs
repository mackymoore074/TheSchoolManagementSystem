using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSchoolManagementSystem.Data;
using TheSchoolManagementSystem.Models;

namespace TheSchoolManagementSystem.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministratorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administrator
        public IActionResult Index()
        {
            return View();
        }

        // GET: Students
        public async Task<IActionResult> Students()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        // GET: Students/Create
        public IActionResult CreateStudent()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Students)); // Redirect to Students list
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Log the exception for debugging
                    Console.WriteLine("Concurrency exception occurred.");
                    throw;
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the student.");
                }
            }
            else
            {
                // Log validation errors
                foreach (var modelStateEntry in ModelState)
                {
                    foreach (var error in modelStateEntry.Value.Errors)
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}"); // Log each validation error
                    }
                }
            }

            // If we got here, something went wrong with the form data
            return View(student);
        }

        // GET: Teachers
        public async Task<IActionResult> Teachers()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }

        // GET: Teachers/Create
        public IActionResult CreateTeacher()
        {
            return View();
        }

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Teachers)); // Redirect to Teachers list
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Log the exception for debugging
                    Console.WriteLine("Concurrency exception occurred.");
                    throw;
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the teacher.");
                }
            }

            // If we got here, something went wrong with the form data
            return View(teacher);
        }
    }
}
