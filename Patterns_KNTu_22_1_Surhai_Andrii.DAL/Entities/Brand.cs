namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Brand()
        {

        }

        public class Builder
        {
            private Brand brand = new Brand();

            public Builder WithId(int id)
            {
                brand.Id = id;
                return this;
            }

            public Builder WithName(string name)
            {
                brand.Name = name;
                return this;
            }

            public Brand Build()
            {
                if (string.IsNullOrEmpty(brand.Name))
                {
                    throw new InvalidOperationException("Name is required.");
                }
                return brand;
            }
        }
    }
}
