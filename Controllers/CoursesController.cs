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
            var Courses = _unitOfWork.Courses.GetAll();
            return View(Courses);
        }

        // GET: Course/Details/5
        public IActionResult Details(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            var departments = _unitOfWork.Departments.GetAll();
            ViewBag.DepartmentList = departments;
            System.Diagnostics.Debug.WriteLine("Departments count: " + departments.Count());
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name, Hours, DepartmentID")] Course Courses)
        {
            if (ModelState.IsValid)
            {
            }
            _unitOfWork.Courses.Insert(Courses);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
            return View(Courses);
        }

        // GET: Course/Edit/5
        public IActionResult Edit(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);

            if (course == null)
            {
                return NotFound();
            }
            var departments = _unitOfWork.Departments.GetAll();
            ViewBag.DepartmentList = departments;
            System.Diagnostics.Debug.WriteLine("Departments count: " + departments.Count());
            return View();
            return View(course);
        }


        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID, Name, Hours, DepartmentID")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
              
            }
            try
            {
                _unitOfWork.Courses.Update(course);
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.Courses.GetAll().Any(e => e.ID == course.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            return View(course);
        }

        // GET: Course/Delete/5
        public IActionResult Delete(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.Courses.SoftDelete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        // Hard delete
        [HttpPost]
        public IActionResult HardDelete(int id)
        {
            _unitOfWork.Courses.HardDelete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
