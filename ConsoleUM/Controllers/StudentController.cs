using ConsoleUM.Data;
using ConsoleUM.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUM.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context) { _context = context; }
        public async Task<IActionResult> Index()
        {
            var result = await _context.Students.ToListAsync();


            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null) { return NotFound(); }

            var student = await _context.Students.Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex) { ModelState.AddModelError("", "Unable to save changes. Try again and if problem persists see your systemc administrator"); }

            return View(student);
        }
    }
}
