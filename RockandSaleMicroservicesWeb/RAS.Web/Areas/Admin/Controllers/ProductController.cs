using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAS.Web.Models;
using RAS.Web.Services.IServices;
using RAS.Web.ViewModels;
using System.Data;
using System.Reflection;

namespace RAS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public IWebHostEnvironment _environment;
        public ProductController(IProductService productService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetAllProductsAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

//[HttpPost]
        //
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            string yuklenenResimAdi = UploadIMG(model);
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            ProductDto productDto = new ProductDto
            {
                ProductId = model.ProductId,
                Brand = model.Brand,
                Model = model.Model,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = yuklenenResimAdi,
                Category = model.Category,
                Count = model.Count
            };

            var response = await _productService.CreateProductAsync<ResponseDto>(productDto, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                ProductViewModel productViewModel = new ProductViewModel
                {
                ProductId = model.ProductId,
                Brand = model.Brand,
                Model = model.Model,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Category = model.Category,
                Count = model.Count
                };

            return View(productViewModel);
        }
			return NotFound();
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductViewModel model)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var guncellencekUrun = await _productService.GetProductByIdAsync<ResponseDto>(model.ProductId, accessToken);

            if (guncellencekUrun != null && guncellencekUrun.IsSuccess)
            {
                ProductDto model2 = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(guncellencekUrun.Result));

                if (model.ProductPicture != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "Uploads", model2.ImageUrl);
                    System.IO.File.Delete(filePath);
                    string yuklenenResimAdi = UploadIMG(model);
                    model2.Brand = model.Brand;
                    model2.Model = model.Model;
                    model2.Price = model.Price;
                    model2.Description = model.Description;
                    model2.ImageUrl = yuklenenResimAdi;
                    model2.Category = model.Category;
                    model2.Count = model.Count;
                    var response = await _productService.UpdateProductAsync<ResponseDto>(model2, accessToken);
                    if (response != null && response.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            if (productId == null)
            {
                return NotFound();
            }


            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductAsync<ResponseDto>(productId, accessToken);
            if (response.IsSuccess && response.Result == "true")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private string UploadIMG(ProductViewModel model)
        {
            string dosyaAdi = "";
            string dosyaninYuklenecegiKlasorYolu = Path.Combine(_environment.WebRootPath, "Uploads");

            if (!Directory.Exists(dosyaninYuklenecegiKlasorYolu))
            {
                Directory.CreateDirectory(dosyaninYuklenecegiKlasorYolu);
            }

            //if (model.ProductPicture.FileName != null)
            //{
            //    dosyaAdi = model.ProductPicture.FileName;
            //    string filePath = Path.Combine(dosyaninYuklenecegiKlasorYolu, dosyaAdi);
            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        model.ProductPicture.CopyTo(fileStream);
            //    }

            //}
            return dosyaAdi;
        }
        //------------------
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> ProductDelete(int productId)
        //{
        //    var accessToken = await HttpContext.GetTokenAsync("access_token");
        //    var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
        //    if (response != null && response.IsSuccess)
        //    {
        //        ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
        //        return View(model);
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ProductDelete(ProductDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var accessToken = await HttpContext.GetTokenAsync("access_token");
        //        var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
        //        if (response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(model);
        //}
    }
}
