using MVC_PROJECT.Models;
using MVC_PROJECT.Repositories;

namespace MVC_PROJECT.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Student> Students { get; }
        IRepository<Department> Departments { get; }
        IRepository<Course> Courses { get; }
        void Save();
    }
}

