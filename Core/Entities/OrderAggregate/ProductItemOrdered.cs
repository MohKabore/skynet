namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemOrderedId, string productName, string puctureUrl)
        {
            ProductItemOrderedId = productItemOrderedId;
            ProductName = productName;
            PuctureUrl = puctureUrl;
        }

        public int ProductItemOrderedId { get; set; }
        public string   ProductName { get; set; }
        public string   PuctureUrl { get; set; }
    }
}