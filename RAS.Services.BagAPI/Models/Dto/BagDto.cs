namespace RAS.Services.BagAPI.Models.Dto
{
    public class BagDto
    {
        public BagHeaderDto BagHeader { get; set; }
        public IEnumerable<BagDetailsDto> BagDetails { get; set; }
    }
}
