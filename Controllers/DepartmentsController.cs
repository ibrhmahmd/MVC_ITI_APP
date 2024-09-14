using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;
using MVC_PROJECT.UnitOfWork;
using MVC_PROJECT.Models.DTOs;

namespace MVC_PROJECT.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.Departments.GetAll();
            return View(departments);
        }

        // GET: Departments/Details/5
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
            var allCourses = _unitOfWork.Courses.GetAll();
            ViewBag.AvailableCourses = new SelectList(allCourses, "CourseId", "CourseName");
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Department department)
        {
            if (ModelState.IsValid)
            {
              
            }
            _unitOfWork.Departments.Insert(department);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);

            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("DepartmentId, Name, Courses")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                
            }
            try
            {
                _unitOfWork.Departments.Update(department);
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.Departments.GetAll().Any(e => e.DepartmentId == department.DepartmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            return View(department);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.Departments.SoftDelete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // Hard delete
        [HttpPost]
        public IActionResult HardDelete(int id)
        {
            _unitOfWork.Departments.HardDelete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
