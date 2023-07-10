using System.ComponentModel.DataAnnotations.Schema;

namespace RAS.Services.BagAPI.Models.Dto
{
    public class BagDetailsDto
    {
        public int BagDetailsId { get; set; }
        public int BagHeaderId { get; set; }
        public virtual BagHeaderDto BagHeader { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
