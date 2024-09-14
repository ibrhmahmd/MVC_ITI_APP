using MVC_PROJECT.Models.DTOs;

namespace MVC_PROJECT.Repositories
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void SoftDelete(int id);
        void HardDelete(int id);
        void Save();
    }
}
