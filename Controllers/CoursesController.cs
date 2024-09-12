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
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var course = _unitOfWork.Courses.GetAll()
                              .Select(s => new CourseDTO
                              {
                                  Name = s.Name,
                                  Hours = s.Hours,
                                  DepartmentID = s.DepartmentID,
                              }).ToList();
            return View(course);
        }

        [HttpPost]
        public IActionResult Create(CourseDTO CourseDTO)
        {
            if (ModelState.IsValid)
            {
                var Course = new Course
                {
                    Name = CourseDTO.Name,
                    Hours=CourseDTO.Hours,
                    DepartmentID=CourseDTO.DepartmentID,
                };

                _unitOfWork.Courses.Insert(Course);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(CourseDTO);
        }
    }





















        //    private readonly MyDbContext _context;

        //    public CoursesController(MyDbContext context)
        //    {
        //        _context = context;
        //    }

        //    // GET: CoursesDTO
        //    public async Task<IActionResult> Index()
        //    {
        //        return View(await _context.CourseDTO.ToListAsync());
        //    }

        //    // GET: CoursesDTO/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var courseDTO = await _context.CourseDTO
        //            .FirstOrDefaultAsync(m => m.ID == id);
        //        if (courseDTO == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(courseDTO);
        //    }

        //    // GET: CoursesDTO/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: CoursesDTO/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("ID,Name,Hours,StudentID,DepartmentID")] CourseDTO courseDTO)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(courseDTO);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(courseDTO);
        //    }

        //    // GET: CoursesDTO/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var courseDTO = await _context.CourseDTO.FindAsync(id);
        //        if (courseDTO == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(courseDTO);
        //    }

        //    // POST: CoursesDTO/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Hours,StudentID,DepartmentID")] CourseDTO courseDTO)
        //    {
        //        if (id != courseDTO.ID)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(courseDTO);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!CourseDTOExists(courseDTO.ID))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(courseDTO);
        //    }

        //    // GET: CoursesDTO/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var courseDTO = await _context.CourseDTO
        //            .FirstOrDefaultAsync(m => m.ID == id);
        //        if (courseDTO == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(courseDTO);
        //    }

        //    // POST: CoursesDTO/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var courseDTO = await _context.CourseDTO.FindAsync(id);
        //        if (courseDTO != null)
        //        {
        //            _context.CourseDTO.Remove(courseDTO);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool CourseDTOExists(int id)
        //    {
        //        return _context.CourseDTO.Any(e => e.ID == id);
        //    }
        //}
    }
