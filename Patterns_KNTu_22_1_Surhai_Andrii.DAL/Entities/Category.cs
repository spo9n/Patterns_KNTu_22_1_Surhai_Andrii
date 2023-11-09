namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private Category()
        {

        }

        public class Builder
        {
            private Category category = new Category();

            public Builder WithId(int id)
            {
                category.Id = id;
                return this;
            }

            public Builder WithName(string name)
            {
                category.Name = name;
                return this;
            }

            public Category Build()
            {
                if (string.IsNullOrEmpty(category.Name))
                {
                    throw new InvalidOperationException("Name is required.");
                }
                return category;
            }
        }
    }
}
