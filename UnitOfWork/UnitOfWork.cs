using MVC_PROJECT.Models;
using MVC_PROJECT.Repositories;

namespace MVC_PROJECT.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
        private IRepository<Student> _students;
        private IRepository<Department> _departments;
        private IRepository<Course> _courses;

        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }

        // Properties to access repositories
        public IRepository<Student> Students => _students ??= new StudentRepository(_context);
        public IRepository<Department> Departments => _departments ??= new DepartmentRepository(_context);
        public IRepository<Course> Courses => _courses ??= new CourseRepository(_context);

        // Commit all changes to the database
        public void Save()
        {
            _context.SaveChanges();
        }

        // Dispose the DbContext to free resources
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
