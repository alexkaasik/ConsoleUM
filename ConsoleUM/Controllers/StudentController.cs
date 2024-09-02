using ConsoleUM.Data;
using ConsoleUM.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleUM.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context) { _context = context; }
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["nameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name-desc" : "";
            ViewData["nameSortParm"] = sortOrder == "Date" ? "data_desc" : "Date";

            var student = await _context.Student.ToListAsync();

            switch (sortOrder)
            {
                case "name_desc":
                    student = student.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    student = student.OrderByDescending(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    student = student.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    student = student.OrderBy(s => s.LastName);
                    break;
            }
            return View(await student.AsNoTracking().ToListAsync());

            //var result = await _context.Student.ToListAsync();
            //return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var student = await _context.Student
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Student.FirstOrDefaultAsync(s => s.Id == id);
            
            _context.Update(studentToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null) { return NotFound(); }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Deletion has failed try again";
            }

            return View(student);
        }
        public async Task<IActionResult> DeleteConformtion(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null) 
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) { 
            
            
            }
        }
    }
}
