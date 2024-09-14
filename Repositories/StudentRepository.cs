using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;
using MVC_PROJECT.Models.DTOs;

namespace MVC_PROJECT.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly MyDbContext _context;
        private readonly DbSet<Student> _dbSet;

        public StudentRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Student>();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students
                            .Include(s => s.Department) // Include related entities if needed
                            .Where(s => !s.IsDeleted).ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students
                           .Include(s => s.Department) // Include related entities if needed
                           .SingleOrDefault(s => s.StudentId == id && !s.IsDeleted);
        }

        public void Insert(Student entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(Student entity)
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
                _context.Students.Remove(entity);
                _context.SaveChanges();
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
