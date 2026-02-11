using Inventory.Domain.Entities;
using Inventory.Helpers;
using Inventory.Repository.Abstractions;
using Inventory.Services.Abstractions;

namespace Inventory.Services.Implementations
{
    public class ProductService(IProductRepository repo) : IProductService
    {
        private readonly IProductRepository _repo = repo;

        public IEnumerable<Product> GetAll()
        {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public Product GetById(int id)
        {
            try
            {
                var product = _repo.GetById(id);

                if (product is null)
                    throw new InvalidOperationException($"Product with id: {id} not found.");
                
                return product;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public void Add(Product product)
        {
            try
            {
                _repo.Add(product);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public void Update(Product product)
        {
            try
            {
                _repo.Update(product);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

    }
}
