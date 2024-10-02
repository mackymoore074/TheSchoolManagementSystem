using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSchoolManagementSystem.Models; // Adjust the namespace based on your project structure
using System.Threading.Tasks;
using TheSchoolManagementSystem.Data;

namespace TheSchoolManagementSystem.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Registration
        public async Task<IActionResult> Index()
        {
            var registrations = await _context.Registrations
                .Include(r => r.Student) // Include related Student
                .Include(r => r.Subject) // Include related Subject
                .ToListAsync();
            return View(registrations);
        }

        // GET: Registration/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var registration = await _context.Registrations
                .Include(r => r.Student)
                .Include(r => r.Subject)
                .FirstOrDefaultAsync(r => r.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        // GET: Registration/Create
        public IActionResult Create()
        {
            ViewBag.Students = _context.Students.ToList(); // List of Students for dropdown
            ViewBag.Subjects = _context.Subjects.ToList(); // List of Subjects for dropdown
            return View();
        }

        // POST: Registration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Subjects = _context.Subjects.ToList();
            return View(registration);
        }

        // GET: Registration/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Subjects = _context.Subjects.ToList();
            return View(registration);
        }

        // POST: Registration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Subjects = _context.Subjects.ToList();
            return View(registration);
        }

        // GET: Registration/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var registration = await _context.Registrations
                .Include(r => r.Student)
                .Include(r => r.Subject)
                .FirstOrDefaultAsync(r => r.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationId == id);
        }
    }
}
