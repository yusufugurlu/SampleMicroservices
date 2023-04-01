namespace SampleMicroservices.Services.Basket.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public int CourceId { get; set; }
        public string CourceName { get; set; }
        public decimal Price { get; set; }
    }
}
