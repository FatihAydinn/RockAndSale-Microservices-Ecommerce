using RAS.Web.Models;

namespace RAS.Web.Services.IServices
{
	public interface IBagService
	{
		Task<T> GetBagByUserIdAsync<T>(string userId, string token);
		Task<T> AddToBagAsync<T>(BagDto BagDto, string token);
		Task<T> UpdateBagAsync<T>(BagDto BagDto, string token);
		Task<T> RemoveFromBagAsync<T>(int BagId, string token);

		Task<T> Checkout<T>(BagHeaderDto BagHeader, string token);
	}
}
