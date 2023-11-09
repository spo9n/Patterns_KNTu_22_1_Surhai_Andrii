namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        private Order()
        {

        }

        public class Builder
        {
            private Order order = new Order();

            public Builder WithId(int id)
            {
                order.Id = id;
                return this;
            }

            public Builder WithUserId(int userId)
            {
                order.UserId = userId;
                return this;
            }

            public Builder WithStatusId(int statusId)
            {
                order.StatusId = statusId;
                return this;
            }

            public Builder WithComment(string comment)
            {
                order.Comment = comment;
                return this;
            }

            public Order Build()
            {
                if (order.Id == null || order.UserId == null || order.StatusId == null || string.IsNullOrEmpty(order.Comment))
                {
                    throw new InvalidOperationException("Fields are required.");
                }
                return order;
            }
        }
    }
}
