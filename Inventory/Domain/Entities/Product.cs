namespace Inventory.Domain.Entities
{
    public class Product :EntityBase
    {
        // Constructors
        #region Constructors
        public Product(string name, string? description, decimal price)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(Name)} is required");

            if (price < 0)
                throw new ArgumentException($"{nameof(Price)} must be greather than 0");

            Name = name;
            Description = description;
            Price = price;
        }
        #endregion

        // Properties
        #region Properties
        public string Name { get; private set; }
        public string? Description { get; private set; } = String.Empty;
        public decimal Price { get; private set; }
        #endregion

        // Methods
        #region Methods
        public void Update(string name, string? description, decimal price)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(Name)} is required");

            if (price < 0)
                throw new ArgumentException($"{nameof(Price)} must be greather than 0");

            Name = name;
            Description = description;
            Price = price;
        }
        #endregion
    }
}
