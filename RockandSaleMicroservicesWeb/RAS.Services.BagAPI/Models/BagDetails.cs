using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace RAS.Services.BagAPI.Models
{
    public class BagDetails
    {
        public int BagDetailsId { get; set; }
        public int BagHeaderId { get; set; }
        [ForeignKey("BagHeaderId")]
        public virtual BagHeader BagHeader { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
