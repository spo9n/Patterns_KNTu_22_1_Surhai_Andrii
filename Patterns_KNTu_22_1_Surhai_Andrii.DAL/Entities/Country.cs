namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Country()
        {

        }

        public class Builder
        {
            private Country country = new Country();

            public Builder WithId(int id)
            {
                country.Id = id;
                return this;
            }

            public Builder WithName(string name)
            {
                country.Name = name;
                return this;
            }

            public Country Build()
            {
                if (string.IsNullOrEmpty(country.Name))
                {
                    throw new InvalidOperationException("Name is required.");
                }
                return country;
            }
        }
    }
}
