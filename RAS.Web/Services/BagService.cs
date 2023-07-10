using NuGet.Common;
using RAS.Web.Models;
using RAS.Web.Services.IServices;
using System.Reflection.PortableExecutable;

namespace RAS.Web.Services
{
	public class BagService : BaseService, IBagService
	{
		private readonly IHttpClientFactory _clientFactory;
		public BagService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<T> AddToBagAsync<T>(BagDto bagDto, string token/* = null*/)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = bagDto,
				Url = SD.BagAPIBase + "/api/bag",/*/AddBag*/
				AccessToken = token
			});
		}

		public async Task<T> Checkout<T>(BagHeaderDto bagHeader, string token/* = null*/)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = bagHeader,
				Url = SD.BagAPIBase + "/api/checkout",
				AccessToken = token
			});
		}

		public async Task<T> GetBagByUserIdAsync<T>(string userId, string token/* = null*/)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.BagAPIBase + "/api/bag/GetBag/" + userId,
				AccessToken = token
			});
		}

		public async Task<T> RemoveFromBagAsync<T>(int bagId, string token/* = null*/)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = bagId,
				Url = SD.BagAPIBase + "/api/bag/RemoveBag",
				AccessToken = token
			});
		}

		public async Task<T> UpdateBagAsync<T>(BagDto bagDto, string token/* = null*/)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = bagDto,
				Url = SD.BagAPIBase + "/api/bag/UpdateBag",
				AccessToken = token
			});
		}
	}
}
