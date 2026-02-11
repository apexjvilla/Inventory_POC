using Inventory.Domain.Entities;
using Inventory.Repository.Abstractions;

namespace Inventory.Repository.Implementations
{
    public abstract class InMemoryRepositoryBase<T> : IRepositoryBase<T>
        where T : EntityBase
    {
        protected List<T> Data = [];
        private int _curentId = 0;

        public List<T> GetAll() => Data;        

        public T? GetById(int id)
        {
            var entity = Data.FirstOrDefault(e => e.Id == id);

            return entity;
        }

        public void Add(T entity)
        {
            _curentId++;
            entity.Id = _curentId;
            Data.Add(entity);
        }

        public void Update(T entity)
        {
            var existingEntity = GetById(entity.Id);

            if (existingEntity != null)
            {
                var index = Data.IndexOf(existingEntity);

                Data[index] = entity;
            }
            else
            {
                throw new Exception($"{nameof(T)} with id {entity.Id} not found.");
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);

            if (entity != null)
                Data.Remove(entity);
            else
                                throw new Exception($"{nameof(T)} with id {id} not found.");
        }
        
    }
}
