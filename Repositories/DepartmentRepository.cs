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
            return _dbSet.ToList();
        }

        public Department GetById(int id)
        {
            return _dbSet.Find(id);
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

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
