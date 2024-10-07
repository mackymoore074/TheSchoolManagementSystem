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
                return RedirectToAction(nameof(Students));; // Return to the view in case of error
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
           return RedirectToAction(nameof(Teachers));
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
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            return View(subject);
        }

        public async Task<IActionResult> EditSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            var teachers = await _context.Teachers
            .Select(t => new { t.TeacherId, FullName = t.FirstName + " " + t.LastName })
            .ToListAsync();
            
            ViewBag.Teachers = new SelectList(teachers, "TeacherId", "FullName");
            return View(subject);
        }


        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(int id, Subject subject)
        {
             
            // Ensure the ID matches the Subject being edited
            if (id != subject.SubjectId)
            {
                  Console.WriteLine($"ID sent2222: {id}");
                return NotFound();
            }

            Console.WriteLine($"ID sent2222: {id}");
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the subject from the database
                    var subjectToUpdate = await _context.Subjects.FindAsync(id);
                    
                    // Check if the subject exists
                    if (subjectToUpdate == null)
                    {
                        return NotFound();
                    }
                    // Update the subject properties
                    subjectToUpdate.SubjectName = subject.SubjectName;
                    subjectToUpdate.TeacherId = subject.TeacherId;

                    // Save the changes to the database
                    _context.Update(subjectToUpdate);
                    await _context.SaveChangesAsync();

                    // Redirect to the list of subjects after successful update
                    return RedirectToAction(nameof(Subjects)); 
                }
                 catch (DbUpdateConcurrencyException)
                    {
                        Console.WriteLine("Concurrency exception occurred while updating the subject.");
                        ModelState.AddModelError(string.Empty, "A concurrency error occurred.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occurred: {ex.Message}");
                        ModelState.AddModelError(string.Empty, "An error occurred while updating the subject.");
                    }
            }

            // If we get here, something failed; re-display the form
             var teachers = await _context.Teachers
            .Select(t => new { t.TeacherId, FullName = t.FirstName + " " + t.LastName })
            .ToListAsync();
            ViewBag.Teachers = new SelectList(teachers, "TeacherId", "FullName");
            return View(subject);
        }


    }
}
