using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAS.Services.BagAPI.DbContexts;
using RAS.Services.BagAPI.Models;
using RAS.Services.BagAPI.Models.Dto;

namespace RAS.Services.BagAPI.Repository
{
    public class BagRepository : IBagRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public BagRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<bool> ClearBag(string userId)
        {
            var bagHeaderFromDb = await _db.BagHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            if (bagHeaderFromDb != null)
            {
                _db.BagDetails
                    .RemoveRange(_db.BagDetails.Where(u => u.BagHeaderId == bagHeaderFromDb.BagHeaderId));
                _db.BagHeaders.Remove(bagHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<BagDto> CreateUpdateBag(BagDto bagDto)
        {
            Bag bag = _mapper.Map<Bag>(bagDto);
            var productInDb = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == bagDto.BagDetails.FirstOrDefault().ProductId);
            if (productInDb == null)
            {
                _db.Products.Add(bag.BagDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();   
            }
            //BagHeader null olup olmadığının kontrolü
            var bagHeaderFromDb = await _db.BagHeaders.AsNoTracking().FirstOrDefaultAsync(u=>u.UserId == bag.BagHeader.UserId);
            if (bagHeaderFromDb == null)
            {
                //Header ve detail oluşturulur
                _db.BagHeaders.Add(bag.BagHeader);
                await _db.SaveChangesAsync();
                bag.BagDetails.FirstOrDefault().BagHeaderId = bag.BagHeader.BagHeaderId;
                bag.BagDetails.FirstOrDefault().Product = null;
                _db.BagDetails.Add(bag.BagDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //BagHeader null olmadığı durumda
                //detail'de aynı product olup olmadığının kontrolü
                var bagDetailsFromDb = await _db.BagDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == bag.BagDetails.FirstOrDefault().ProductId &&
                    u.BagHeaderId == bagHeaderFromDb.BagHeaderId);

                //aynı üründen yoksa o ürün oluşturulur
                if (bagDetailsFromDb == null)
                {
                    //details oluşturulur
                    bag.BagDetails.FirstOrDefault().BagHeaderId = bagHeaderFromDb.BagHeaderId;
                    bag.BagDetails.FirstOrDefault().Product = null;
                    _db.BagDetails.Add(bag.BagDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //aynı üründen varsa o ürüne sayaç eklenir
                    bag.BagDetails.FirstOrDefault().Product = null;
                    bag.BagDetails.FirstOrDefault().Count += bagDetailsFromDb.Count;
                    bag.BagDetails.FirstOrDefault().BagDetailsId = bagDetailsFromDb.BagDetailsId;
                    bag.BagDetails.FirstOrDefault().BagHeaderId = bagDetailsFromDb.BagHeaderId;
                    _db.BagDetails.Update(bag.BagDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }
            return _mapper.Map<BagDto>(bag);
        }



        public async Task<BagDto> GetBagbyUserId(string userId)
        {
            Bag bag = new Bag()
            { 
                BagHeader = await _db.BagHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
            };
            bag.BagDetails = _db.BagDetails.Where(u => u.BagHeaderId == bag.BagHeader.BagHeaderId).Include(u => u.Product);
            return _mapper.Map<BagDto>(bag);
        }

        public async Task<bool> RemoveFromBag(int bagDetailsId)
        {
            try
            {
                BagDetails bagDetails = await _db.BagDetails
                    .FirstOrDefaultAsync(u => u.BagDetailsId == bagDetailsId);

                int totalCountOfBagItems = _db.BagDetails
                    .Where(u => u.BagHeaderId == bagDetails.BagHeaderId).Count();

                _db.BagDetails.Remove(bagDetails);
                if (totalCountOfBagItems == 1)
                {
                    var bagHeaderToRemove = await _db.BagHeaders
                        .FirstOrDefaultAsync(u => u.BagHeaderId == bagDetails.BagHeaderId);

                    _db.BagHeaders.Remove(bagHeaderToRemove);
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
