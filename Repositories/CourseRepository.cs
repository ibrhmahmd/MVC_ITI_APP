using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;
using MVC_PROJECT.Models.DTOs;

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
            return _dbSet.Where(s => !s.IsDeleted).ToList();
        }

        public Course GetById(int id)
        {
            return _dbSet.SingleOrDefault(s => s.ID == id && !s.IsDeleted);
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
                _context.Courses.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
