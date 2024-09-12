using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.Models;

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
            return _dbSet.ToList();
        }

        public Student GetById(int id)
        {
            return _dbSet.Find(id);
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
