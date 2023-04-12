namespace SampleMicroservices.Services.Basket.Dtos
{
    public class BasketDto
    {
        public BasketDto()
        {
            BasketItems = new();
        }
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
 
        public decimal TotalPrice
        {
            get { return BasketItems.Sum(x=>x.Price*x.Quantity); }
        }

    }
}
