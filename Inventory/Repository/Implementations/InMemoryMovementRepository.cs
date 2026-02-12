using Inventory.Domain.Entities;
using Inventory.Repository.Abstractions;

namespace Inventory.Repository.Implementations
{
    public class InMemoryMovementRepository : IMovementWriteRepository, IMovementReadRepository
    {
        private List<Movement> Data = [];
        private int _movementId = 0;

        public void Add(Movement movement)
        {
            _movementId++;

            movement.Id = _movementId;

            Data.Add(movement);

            var x = Data;
        }

        public IReadOnlyList<Movement> GetAll() => Data.AsReadOnly();

        public Movement? GetById(int movementId) =>
            Data.FirstOrDefault(x => x.Id == movementId);
    }
}
