using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAS.Services.ProductAPI.DbContexts;
using RAS.Services.ProductAPI.Models;
using RAS.Services.ProductAPI.Models.Dto;

namespace RAS.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        //IMapper: DTOlar ile entityleri map eder(eşleştirir) - dışarıya sunarken product classını productDto ile eşleştirir
        //Verileri dışarı sunarken DTO üzerinden sunar.
        //Verileri alırken DTO alır ama database yazarken Product olarak ekler.

        //24.03 - 47:00
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //Kendisine ProductDto tipinde parametre geldiği zaman
        //mapper ProductDto ile Product eşleştirir
        //kayıt eklendikten sonra veritabanından eklenen Product objesi geriye ProductDto oalrak döner

        //public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        //{
        //    Product product = _mapper.Map<ProductDto,Product>(productDto);
        //    if (product.ProductId > 0)
        //    {
        //        _db.Products.Update(product);
        //    }
        //    else
        //    {
        //        _db.Products.Add(product);
        //    }
        //    await _db.SaveChangesAsync();
        //    return _mapper.Map<Product, ProductDto>(product);
        //}
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto,Product>(productDto);
            //Gelen productdto nun içindeki productid 0'dan büyük ise güncelleme değilse yeni kayıt
            if (product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            //kayıt eklendikten sonra databaseden eklenen product objesi geriye productdto olarak döndürülür
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                //Veritabanında olup olmadığının kontrolü sağlanır - FirstOrDefaultAsync
                Product product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product == null)
                {
                    return false;   
                }
                else
                {
                   _db.Products.Remove(product);
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public async Task<ProductDto> GetProductById(int productId)
        //{
        //    //LinQ select * from Product where Id=productId
        //    //{Id:1,Model:1}
        //    Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        //    //Gelen obje ProductDto ya çevirilir
        //    return _mapper.Map<ProductDto>(product);
        //}

        public async Task<ProductDto> GetProductById(int productId)
        {
            //linq select * from Product where ID=productID
            //{ID:1,Name:Product1}
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}
