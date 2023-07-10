using RAS.Services.BagAPI.Models.Dto;

namespace RAS.Services.BagAPI.Repository
{
    public interface IBagRepository
    {
        Task<BagDto> GetBagbyUserId(string userId);
        Task<BagDto> CreateUpdateBag(BagDto bagDto);
        Task<bool> RemoveFromBag(int bagDetailsId);
        Task<bool> ClearBag(string userId);
    }
}
