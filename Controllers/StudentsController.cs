using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;

namespace MVC_PROJECT.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MyDbContext _context;

        public StudentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Students.Include(s => s.Department);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("FirstName,LastName,DateOfBirth,Email,Password,CPassword,DepartmentId")] StudentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                Password = dto.Password,
                DepartmentId = dto.DepartmentId,
            };
            _context.Add(student);
            _context.SaveChanges();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", dto.DepartmentId);
            return RedirectToAction(nameof(Index));
        }


        //// POST: Students/Create
        //[HttpPost]
        //public async Task<IActionResult> CreateStudent([Bind("FirstName,LastName,DateOfBirth,Email,Password,CPassword,DepartmentId")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(student);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", student.DepartmentId);
        //    return View(student);
        //}


        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("{id}")]
        public IActionResult Edit( int id, [Bind("FirstName,LastName,DateOfBirth,Email,DepartmentId,Password,CPassword")] StudentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = new Student { 
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                DepartmentId = dto.DepartmentId,
                Password = dto.Password,
            };
            try
            {
                _context.Update(student);
                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.StudentId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", student.DepartmentId);
            return View(student);
        }



        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
