namespace RAS.Services.OrderAPI.Messages
{
    public class BagDetailsDto
    {
        public int BagDetailsId { get; set; }
        public int BagHeaderId { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
