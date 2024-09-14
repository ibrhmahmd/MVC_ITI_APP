using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;
using MVC_PROJECT.Models.DTOs;

namespace MVC_PROJECT.Repositories
{   
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly MyDbContext _context;
        private readonly DbSet<Department> _dbSet;

        public DepartmentRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Department>();
        }

        public IEnumerable<Department> GetAll()
        {
           var departments = _context.Departments
                .Include(s => s.Courses) // Include related entities if needed
                .Where(s => !s.IsDeleted).ToList();

            return departments;

        }

        public Department GetById(int id)
        {
            return _context.Departments
                            .Include(s => s.Courses) // Include related entities if needed
                            .SingleOrDefault(s => s.DepartmentId == id && !s.IsDeleted);
        }

        public void Insert(Department entity)
        {
            _dbSet.Add(entity);
        }


        public void Update(Department entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void SoftDelete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.SaveChanges();
            }
        }
        public void HardDelete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _context.Departments.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
