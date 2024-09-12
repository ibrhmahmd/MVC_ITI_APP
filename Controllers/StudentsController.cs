using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;
using MVC_PROJECT.Models.DTOs;
using MVC_PROJECT.UnitOfWork;

namespace MVC_PROJECT.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var students = _unitOfWork.Students.GetAll()
                              .Select(s => new StudentDTO
                              {
                                  StudentId = s.StudentId,
                                  FirstName = s.FirstName,
                                  LastName = s.LastName,
                                  Email = s.Email,  
                                  DateOfBirth = s.DateOfBirth,
                                  DepartmentId = s.DepartmentId
                              }).ToList();
            return View(students);
        }

        [HttpPost]
        public IActionResult Create(StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = studentDTO.FirstName,
                    LastName = studentDTO.LastName,
                    Email = studentDTO.Email,
                    DateOfBirth = studentDTO.DateOfBirth,
                    DepartmentId = studentDTO.DepartmentId
                };

                _unitOfWork.Students.Insert(student);
                _unitOfWork.Save(); 
                return RedirectToAction(nameof(Index));
            }
            return View(studentDTO);
        }















        //private readonly MyDbContext _context;

        //public StudentsController(MyDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: Students
        //public async Task<IActionResult> Index()
        //{
        //    var myDbContext = _context.Students.Include(s => s.Department);
        //    return View(await myDbContext.ToListAsync());
        //}

        //// GET: Students/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students
        //        .Include(s => s.Department)
        //        .FirstOrDefaultAsync(m => m.StudentId == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        //// GET: Students/Create
        //public IActionResult Create()
        //{
        //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create([Bind("FirstName,LastName,DateOfBirth,Email,Password,CPassword,DepartmentId")] StudentDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (!EmailExists(dto.Email))
        //    {
        //        var student = new Student
        //        {
        //            FirstName = dto.FirstName,
        //            LastName = dto.LastName,
        //            DateOfBirth = dto.DateOfBirth,
        //            Email = dto.Email,
        //            Password = dto.Password,
        //            DepartmentId = dto.DepartmentId,
        //        };
        //        _context.Add(student);
        //        _context.SaveChanges();
        //        return Json(new { success = true, redirectUrl = Url.Action("Index") });
        //    }
        //    return Json(new { success = false, message = "This email is used before" });
        //}


        //// GET: Students/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var student = await _context.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", student.DepartmentId);
        //    return View(student);
        //}


        //// POST: Students/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,DateOfBirth,Email,DepartmentId,Password,CPassword")] StudentDTO studentDTO)
        //{
        //    if (id != studentDTO.StudentId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var student = await _context.Students.FindAsync(id);
        //            if (student == null)
        //            {
        //                return NotFound();
        //            }
        //            if (!EmailExists(studentDTO.Email))
        //            {
        //                //student properties from DTO
        //                student.FirstName = studentDTO.FirstName;
        //                student.LastName = studentDTO.LastName;
        //                student.DateOfBirth = studentDTO.DateOfBirth;
        //                student.Email = studentDTO.Email;
        //                student.DepartmentId = studentDTO.DepartmentId;

        //                // password if a new one is provided
        //                if (!string.IsNullOrEmpty(studentDTO.Password))
        //                {
        //                    student.Password = studentDTO.Password;
        //                }
        //                _context.Update(student);
        //                await _context.SaveChangesAsync();
        //            }
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StudentExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return Json(new { success = false, message = "This email is used before" });
        //}

        //// GET: Students/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students
        //        .Include(s => s.Department)
        //        .FirstOrDefaultAsync(m => m.StudentId == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}

        //// POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var student = await _context.Students.FindAsync(id);
        //    if (student != null)
        //    {
        //        _context.Students.Remove(student);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool StudentExists(int id)
        //{
        //    return _context.Students.Any(e => e.StudentId == id);
        //}

        //private bool EmailExists(string email) 
        //{
        //    return _context.Students.Any(e => e.Email== email);
        //}
    }
}