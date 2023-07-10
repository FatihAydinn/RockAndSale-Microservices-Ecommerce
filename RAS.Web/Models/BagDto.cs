namespace RAS.Web.Models
{
	public class BagDto
	{
		public BagHeaderDto BagHeader { get; set; }
		public IEnumerable<BagDetailsDto> BagDetails { get; set; }
	}
}
