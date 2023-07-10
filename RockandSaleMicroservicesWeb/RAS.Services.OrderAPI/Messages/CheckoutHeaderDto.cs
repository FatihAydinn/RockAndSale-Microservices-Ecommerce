namespace RAS.Services.OrderAPI.Messages
{
    public class CheckoutHeaderDto
    {
        public int BagHeaderId { get; set; }
        public string UserId { get; set; }
        public double OrderTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public int BagTotalItems { get; set; }
        public IEnumerable<BagDetailsDto> BagDetails { get; set; }
    }
}
