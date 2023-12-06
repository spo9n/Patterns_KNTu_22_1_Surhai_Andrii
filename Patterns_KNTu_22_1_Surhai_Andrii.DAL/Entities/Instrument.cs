using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Memento;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int CountryId { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }


        private Instrument()
        {

        }

        public InstrumentMemento CreateMemento()
        {
            return new InstrumentMemento(this.Id, this.Name, this.CategoryId, this.BrandId, this.CountryId, 
                this.Year, this.Price, this.Quantity, this.Description);
        }

        public void RestoreMemento(InstrumentMemento instrumentMemento)
        {
            this.Id = instrumentMemento.Id;
            this.Name = instrumentMemento.Name;
            this.CategoryId = instrumentMemento.CategoryId;
            this.BrandId = instrumentMemento.BrandId;
            this.CountryId = instrumentMemento.CountryId;
            this.Year = instrumentMemento.Year;
            this.Price = instrumentMemento.Price;
            this.Quantity = instrumentMemento.Quantity;
            this.Description = instrumentMemento.Description;
        }

        public class Builder
        {
            private Instrument instrument = new Instrument();

            public Builder WithId(int id)
            {
                instrument.Id = id;
                return this;
            }

            public Builder WithName(string name)
            {
                instrument.Name = name;
                return this;
            }

            public Builder WithCategoryId(int category_id)
            {
                instrument.CategoryId = category_id;
                return this;
            }

            public Builder WithBrandId(int brand_id)
            {
                instrument.BrandId = brand_id;
                return this;
            }

            public Builder WithCountryId(int country_id)
            {
                instrument.CountryId = country_id;
                return this;
            }

            public Builder WithYear(int year)
            {
                instrument.Year = year;
                return this;
            }

            public Builder WithPrice(double price)
            {
                instrument.Price = price;
                return this;
            }

            public Builder WithQuantity(int quantity)
            {
                instrument.Quantity = quantity;
                return this;
            }

            public Builder WithDescription(string description)
            {
                instrument.Description = description;
                return this;
            }

            public Instrument Build()
            {
                if (string.IsNullOrEmpty(instrument.Name) || instrument.CategoryId == null || instrument.BrandId == null || instrument.CountryId == null ||
                    instrument.Year == null || instrument.Price == null || instrument.Quantity == null || string.IsNullOrEmpty(instrument.Description))
                {
                    throw new InvalidOperationException("Name and Category are required.");
                }
                return instrument;
            }

            public Instrument BuildEmpty()
            {
                return instrument;
            }
        }

    }
}
