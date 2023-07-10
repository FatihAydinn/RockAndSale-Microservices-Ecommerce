﻿namespace RAS.Services.OrderAPI.Models
{
    public class OrderHeader
    {
        public int OrderHeaderId { get; set; }
        public string UserId { get; set; }
        public double OrderTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime OrderTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public int BagTotalItems { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
