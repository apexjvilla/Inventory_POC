namespace Inventory.Domain.Entities
{
    public class Warehouse : EntityBase
    {
        // Constructors
        #region constructors
        public Warehouse(string name, string location)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(Name)} is required");

            if (String.IsNullOrEmpty(location))
                throw new ArgumentException($"{nameof(Location)} is required");

            Name = name;
            Location = location;
        }
        #endregion

        // Properties
        #region Properties
        public string Name { get; private set; }
        public string Location { get; private set; }
        #endregion

        // Methods
        #region Methods
        public void Update(string name, string location)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(Name)} is required");

            if (String.IsNullOrEmpty(location))
                throw new ArgumentException($"{nameof(Location)} is required");

            Name = name;
            Location = location;
        }
        #endregion
    }
}
