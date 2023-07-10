using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAS.Web.Models;
using RAS.Web.Services.IServices;
using System.Data;

namespace RAS.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
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

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ProductCreate(ProductDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var accessToken = await HttpContext.GetTokenAsync("access_token");
        //        var response = await _productService.CreateProductAsync<ResponseDto>(model, accessToken);
        //        if (response != null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            //if (ModelState.IsValid)
            //  {
            //string yuklenenResimAdi = ResimYukle(model);
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            ProductDto productDto = new ProductDto
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

            var response = await _productService.CreateProductAsync<ResponseDto>(productDto, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            //}
            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
    //[Area("Admin")]
    //public class ProductController : Controller
    //{
    //    public IProductService _productService;

    //    public ProductController(IProductService productService)
    //    {
    //        _productService = productService;
    //    }

    //    public async Task<IActionResult> Index()
    //    {
    //        List<ProductDto> list = new();
    //        //token varlığı kontrol edilecek
    //        var accessToken = await HttpContext.GetTokenAsync("access_token");
    //        //tüm ürünleri getirmeye çalışır
    //        var response = await _productService.GetAllProductsAsync<ResponseDto>(accessToken);

    //        //tüm ürünler gelir ve response null değer döndürmüyorsa
    //        if (response != null && response.IsSuccess)
    //        {
    //            //Json içerisinde gelen veriyi içine productdto tipinde veri alan bir listeye çevirir
    //            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
    //        }
    //        return View(list);
    //    }

    //    public async Task<IActionResult> ProductCreate()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> ProductCreate(ProductDto model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var accessToken = await HttpContext.GetTokenAsync("access_token");
    //            var response = await _productService.CreateProductAsync<ResponseDto>(model, accessToken);
    //            try
    //            {
    //                if (response != null && response.IsSuccess)
    //                {
    //                    return RedirectToAction(nameof(Index));
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                response.IsSuccess = false;
    //                response.ErrorMessages = new List<string>() { ex.ToString() };
    //            }
    //        }
    //        return View(model);
    //    }

    //    public async Task<IActionResult> ProductEdit(int productId)
    //    {
    //        var accessToken = await HttpContext.GetTokenAsync("access_token");
    //        var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
    //        if (response != null && response.IsSuccess)
    //        {
    //            ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
    //            return View(model);
    //        }
    //        return NotFound();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> ProductEdit(ProductDto model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var accessToken = await HttpContext.GetTokenAsync("access_token");
    //            var response = await _productService.UpdateProductAsync<ResponseDto>(model, accessToken);
    //            if (response != null && response.IsSuccess)
    //            {
    //                return RedirectToAction(nameof(Index));
    //            }
    //        }
    //        return View(model);
    //    }

    //    [Authorize(Roles = "Admin")]
    //    public async Task<IActionResult> ProductDelete(int productId)
    //    {
    //        var accessToken = await HttpContext.GetTokenAsync("access_token");
    //        var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
    //        if (response != null && response.IsSuccess)
    //        {
    //            ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
    //            return View(model);
    //        }
    //        return NotFound();
    //    }

    //    [HttpPost]
    //    [Authorize(Roles = "Admin")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> ProductDelete(ProductDto model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var accessToken = await HttpContext.GetTokenAsync("access_token");
    //            var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId, accessToken);
    //            if (response.IsSuccess)
    //            {
    //                return RedirectToAction(nameof(Index));
    //            }
    //        }
    //        return View(model);
    //    }
    //}
}
