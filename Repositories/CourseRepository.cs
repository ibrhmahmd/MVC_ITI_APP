using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;

namespace MVC_PROJECT.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly MyDbContext _context;
        private readonly DbSet<Course> _dbSet;

        public CourseRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Course>();
        }

        public IEnumerable<Course> GetAll()
        {
            return _dbSet.ToList();
        }

        public Course GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(Course entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(Course entity)
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
