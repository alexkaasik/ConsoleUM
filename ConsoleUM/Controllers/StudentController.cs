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
        public async Task<IActionResult> Index(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? pageNumber
        )
        {
            ViewData["nameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name-desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "data_desc" : "Date";
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var student = from s in _context.Student
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            { 
                student = student.Where(s => s.LastName.Contains(searchString)
                                          || s.FirstName.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    student = student.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    student = student.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    student = student.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    student = student.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Student>.CreateAsync(student.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(await student.AsNoTracking().ToListAsync());

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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Student student)
        {
            if (student.Id != student.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem presists " +
                    "see your system administrator.";
            }

            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
