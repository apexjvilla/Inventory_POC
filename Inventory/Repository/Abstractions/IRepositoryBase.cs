using Inventory.Domain.Entities;

namespace Inventory.Repository.Abstractions
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        List<T> GetAll();
        T? GetById(int id);
            void Add(T entity);
            void Update(T entity);
            void Delete(int id);
    }
}
