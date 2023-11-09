namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private OrderStatus()
        {

        }

        public class Builder
        {
            private OrderStatus orderStatus = new OrderStatus();

            public Builder WithId(int id)
            {
                orderStatus.Id = id;
                return this;
            }

            public Builder WithName(string name)
            {
                orderStatus.Name = name;
                return this;
            }

            public OrderStatus Build()
            {
                if (string.IsNullOrEmpty(orderStatus.Name))
                {
                    throw new InvalidOperationException("Name is required.");
                }
                return orderStatus;
            }
        }
    }
}
