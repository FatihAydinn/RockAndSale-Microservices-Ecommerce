using System.ComponentModel.DataAnnotations;

namespace RAS.Services.BagAPI.Models
{
    public class BagHeader
    {
        [Key]
        public int BagHeaderId { get; set; }
        public string UserId { get; set; }
    }
}
