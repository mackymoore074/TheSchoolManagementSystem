using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                try
                {
                    _context.Add(student);
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

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Students)); // Redirect to Students list
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Log the concurrency exception for debugging
                    Console.WriteLine("Concurrency exception occurred.");
                    throw;
                }
                catch (Exception ex)
                {
                    // Log any other exceptions for debugging
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the student.");
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

        // GET: Students/Delete/5
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/DeleteStudentConfirmed
        [HttpPost, ActionName("DeleteStudentConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudentConfirmed(Student student)
        {
            // Get the StudentId from the form submission
            var studentToDelete = await _context.Students.FindAsync(student.StudentId);
            if (studentToDelete == null)
            {
                return NotFound();
            }

            try
            {
                _context.Students.Remove(studentToDelete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Students)); // Redirect to Students list
            }
            catch (DbUpdateException ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the student.");
                return View(studentToDelete); // Return to the view in case of error
            }
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

        // GET: Teachers/Edit/5
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Teachers)); // Redirect to Teachers list after successful update
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("Concurrency exception occurred while updating the teacher.");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the teacher.");
                }
            }

            // If we got here, something went wrong with the form data
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/DeleteTeacherConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeacherConfirmed(Teacher teacher)
        {
            var teachertoDelete = await _context.Teachers.FindAsync(teacher.TeacherId);
            if (teachertoDelete == null)
            {
                return NotFound();
            }

            try
            {
                _context.Teachers.Remove(teachertoDelete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Teachers)); // Redirect to Teachers list after successful update
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("Concurrency exception occurred while updating the teacher.");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the teacher.");
            }

            // If we got here, something went wrong with the form data
            return View(teacher);
        }

        // GET: Registration/Registrations
        public IActionResult Registrations()
        {
            var registrations = _context.Registrations.Include(r => r.Subject).Include(r => r.Student).ToList();
            return View(registrations);
        }

        // GET: Registration/Create
        public IActionResult CreateRegistration()
        {
            ViewBag.Subjects = new SelectList(_context.Subjects.ToList(), "SubjectId", "SubjectName"); // Get subjects to populate dropdown
            ViewBag.Students = new SelectList(_context.Students.ToList(), "StudentId", "FirstName"); // Get students to populate dropdown
            return View();
        }


        // POST: Registration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRegistration(Registration registration)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                try
                {
                    _context.Add(registration); // Add the registration to the context
                    _context.SaveChanges(); // Save changes to the database
                    return RedirectToAction(nameof(Registrations)); // Redirect to the registrations list
                }
                catch (DbUpdateException ex)
                {
                    // Log the error (you could use a logging framework here)
                    Console.WriteLine($"Error saving registration: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists, contact your administrator.");
                }
            }

            // Repopulate dropdowns on failure
            ViewBag.Subjects = new SelectList(_context.Subjects.ToList(), "SubjectId", "SubjectName");
            ViewBag.Students = new SelectList(_context.Students.ToList(), "StudentId", "FirstName");

            return View(registration); // Return the view with the current registration data
        }


        // GET: Registration/Edit/5
        public async Task<IActionResult> EditRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            ViewBag.Subjects = new SelectList(_context.Subjects.ToList(), "SubjectId", "SubjectName"); // Get subjects to populate dropdown
            ViewBag.Students = new SelectList(_context.Students.ToList(), "StudentId", "FirstName"); // Get students to populate dropdown
            return View(registration);
        }

        // POST: Registration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegistration(int id, Registration registration)
        {
            if (id != registration.RegistrationId) // Assuming you have RegistrationId in your model
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Registrations)); // Redirect to Registrations list after successful update
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("Concurrency exception occurred while updating the registration.");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the registration.");
                }
            }

            // Repopulate dropdowns on failure
            ViewBag.Subjects = new SelectList(_context.Subjects.ToList(), "SubjectId", "SubjectName");
            ViewBag.Students = new SelectList(_context.Students.ToList(), "StudentId", "FirstName");
            return View(registration);
        }


        // GET: Registration/Delete/5
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        // POST: Registration/DeleteConfirmed
        [HttpPost, ActionName("DeleteRegistrationConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRegistrationConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Registrations)); // Redirect to Registrations list
        }
        public IActionResult Subjects()
        {
            var subjects = _context.Subjects.Include(s => s.Teacher).ToList();
            return View(subjects);
        }

        // GET: Administrator/CreateSubject
        public async Task<IActionResult> CreateSubject()
        {
            var teachers = await _context.Teachers
            .Select(t => new { t.TeacherId, FullName = t.FirstName + " " + t.LastName })
            .ToListAsync();
            ViewBag.Teachers = teachers;

            return View();
        }

        // POST: Administrator/CreateSubject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                return RedirectToAction(nameof(Subjects));
            }
            // If model is invalid, repopulate the SelectList
            ViewBag.Teachers = new SelectList(_context.Teachers.ToList(), "TeacherId", "Name");
            return View(subject);
        }

        // GET: Administrator/EditSubject/{id}
        public async Task<IActionResult> EditSubject(int id)
        {
            try
            {
                var subject = await _context.Subjects.FindAsync(id);
                if (subject == null)
                {
                    return NotFound();
                }

                // Pre-select the current teacher in the dropdown
                ViewBag.Teachers = new SelectList(_context.Teachers.ToList(), "TeacherId", "FirstName", subject.TeacherId);
                return View(subject);
            }
            catch (Exception ex)
            {
                // Log error and return an error view/message
                Console.WriteLine($"Error fetching subject or teachers: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = "Unable to load subject for editing." });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(int id, Subject subject)
        {
            try
            {
                // Check if the provided subject ID matches the URL ID
                if (id != subject.SubjectId)
                {
                    return NotFound();
                }

                // Ensure the model state is valid
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(subject);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Subjects)); // Redirect after a successful update
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        // Handle concurrency issues
                        if (!_context.Subjects.Any(e => e.SubjectId == id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                // If model validation fails, repopulate the teacher list for the dropdown with the selected teacher pre-selected
                ViewBag.Teachers = new SelectList(_context.Teachers.ToList(), "TeacherId", "FirstName", subject.TeacherId);
                return View(subject); // Return the same view with validation messages
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine($"Error updating subject: {ex.Message}\n{ex.StackTrace}");
                return View("Error", new ErrorViewModel { RequestId = "Unable to edit subject." });
            }
        }

    }

}
