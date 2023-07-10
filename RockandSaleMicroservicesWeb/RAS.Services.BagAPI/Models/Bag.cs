using System.Reflection.PortableExecutable;

namespace RAS.Services.BagAPI.Models
{
    public class Bag
    {
        public BagHeader BagHeader { get; set; }
        public IEnumerable<BagDetails> BagDetails { get; set; }
    }
}
