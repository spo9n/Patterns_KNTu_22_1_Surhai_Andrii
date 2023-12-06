namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Memento
{
    public class InstrumentMemento
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CategoryId { get; private set; }
        public int BrandId { get; private set; }
        public int CountryId { get; private set; }
        public int Year { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public string Description { get; private set; }


        public InstrumentMemento(int id, string name, int categoryId, int brandId, int countryId, int year, double price, int quantity, string description)
        {
            this.Id = id;
            this.Name = name;
            this.CategoryId = categoryId;
            this.BrandId = brandId;
            this.CountryId = countryId;
            this.Year = year;
            this.Price = price;
            this.Quantity = quantity;
            this.Description = description;
        }
    }
}
