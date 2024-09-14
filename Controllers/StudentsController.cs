using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;
using MVC_PROJECT.Models.DTOs;
using MVC_PROJECT.Repositories;
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
            // Student entities from the repository
            var students = _unitOfWork.Students.GetAll();
            return View(students); // Passes Students to the view
        }
        // GET: Students/Details/5
        public IActionResult Details(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        // GET: Students/Create
        public IActionResult Create()
        {
            var departments = _unitOfWork.Departments.GetAll();
            ViewBag.DepartmentList = departments;
            System.Diagnostics.Debug.WriteLine("Departments count: " + departments.Count());
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,DateOfBirth,Email, Password, DepartmentId")] Student Student)
        {
            if (ModelState.IsValid)
            {
            }
            _unitOfWork.Students.Insert(Student);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
            return View(Student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            var departments = _unitOfWork.Departments.GetAll();
            ViewBag.DepartmentList = departments;
            System.Diagnostics.Debug.WriteLine("Departments count: " + departments.Count());
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StudentId, FirstName,LastName,DateOfBirth,Email,Password, DepartmentId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
            }
            try
            {
                _unitOfWork.Students.Update(student);
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_unitOfWork.Students.GetAll().Any(e => e.StudentId == student.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.Students.SoftDelete(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        // Hard delete
        [HttpPost]
        public IActionResult HardDelete(int id)
        {
            _unitOfWork.Students.HardDelete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}