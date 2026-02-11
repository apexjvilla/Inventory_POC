using Inventory.Domain.Entities;
using Inventory.Helpers;
using Inventory.Repository.Abstractions;
using Inventory.Services.Abstractions;

namespace Inventory.Services.Implementations
{
    public class WarehouseService(IWarehouseRepository repo) : IWarehouseService
    {
    private readonly IWarehouseRepository _repo = repo;

    public IEnumerable<Warehouse> GetAll()
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

    public Warehouse GetById(int id)
    {
        try
        {
            return _repo.GetById(id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            throw;
        }
    }

    public void Add(Warehouse Warehouse)
    {
        try
        {
            _repo.Add(Warehouse);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            throw;
        }
    }

    public void Update(Warehouse warehouse)
    {
        try
        {
            _repo.Update(warehouse);
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
