namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int InstrumentId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }


        private OrderDetail()
        {

        }

        public class Builder
        {
            private OrderDetail orderDetail = new OrderDetail();

            public Builder WithOrderId(int orderId)
            {
                orderDetail.OrderId = orderId;
                return this;
            }

            public Builder WithInstrumentId(int instrumentId)
            {
                orderDetail.InstrumentId = instrumentId;
                return this;
            }

            public Builder WithPrice(double price)
            {
                orderDetail.Price = price;
                return this;
            }

            public Builder WithQuantity(int quantity)
            {
                orderDetail.Quantity = quantity;
                return this;
            }

            public OrderDetail Build()
            {
                if (orderDetail.OrderId == null || orderDetail.InstrumentId == null || 
                    orderDetail.Price == null || orderDetail.Quantity == null)
                {
                    throw new InvalidOperationException("Fields are required.");
                }
                return orderDetail;
            }
        }
    }
}
