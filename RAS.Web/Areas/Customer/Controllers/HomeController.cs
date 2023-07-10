using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAS.Web.Models;
using RAS.Web.Services.IServices;
using System.Diagnostics;

namespace RAS.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IBagService _bagService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IBagService bagService)
        {
            _logger = logger;
            _productService = productService;
            _bagService = bagService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }


        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto model = new();
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, "");
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            BagHeaderDto bagHeaderDto = new BagHeaderDto();
            bagHeaderDto.UserId = UserId;
            BagDto bagDto = new BagDto();
            bagDto.BagHeader = bagHeaderDto;


            BagDetailsDto bagDetails = new BagDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };

            var resp = await _productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId, "");
            if (resp != null && resp.IsSuccess)
            {
                bagDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resp.Result));
            }
            List<BagDetailsDto> bagDetailsDtos = new();
            bagDetailsDtos.Add(bagDetails);
            bagDto.BagDetails = bagDetailsDtos;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToBagResp = await _bagService.AddToBagAsync<ResponseDto>(bagDto, accessToken);

            if (addToBagResp != null && addToBagResp.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var role = User.Claims.Where(u => u.Type == "role")?.FirstOrDefault()?.Value;

            if (role == "Admin")
            {
                // return Redirect("~/Admin/Admin");
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }

            //buradan IdentityServer daki login sayfasına gidiliyor.
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}