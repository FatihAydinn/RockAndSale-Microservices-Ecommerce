using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAS.Web.Models;
using RAS.Web.Services.IServices;

namespace RAS.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BagController : Controller
    {

        private readonly IProductService _productService;
        private readonly IBagService _bagService;
        public BagController(IProductService productService, IBagService bagService)
        {
            _productService = productService;
            _bagService = bagService;
        }
        public async Task<IActionResult> BagIndex()
        {
            return View(await LoadBagDtoBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int bagDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _bagService.RemoveFromBagAsync<ResponseDto>(bagDetailsId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(BagIndex));
            }
            return View();
        }


        public async Task<IActionResult> Checkout()
        {
            return View(await LoadBagDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(BagDto bagDto)
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _bagService.Checkout<ResponseDto>(bagDto.BagHeader, accessToken);
                // var response = await _bagService.Checkout2<ResponseDto>(bagDto, accessToken);
                if (!response.IsSuccess)
                {
                    TempData["Error"] = response.DisplayMessage;
                    return RedirectToAction(nameof(Checkout));
                }
                return RedirectToAction(nameof(Confirmation));
            }
            catch (Exception e)
            {
                return View(bagDto);
            }
        }

        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
        private async Task<BagDto> LoadBagDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _bagService.GetBagByUserIdAsync<ResponseDto>(userId, accessToken);

            BagDto bagDto = new();
            if (response != null && response.IsSuccess)
            {
                bagDto = JsonConvert.DeserializeObject<BagDto>(Convert.ToString(response.Result));
            }

            if (bagDto.BagHeader != null)
            {
                foreach (var detail in bagDto.BagDetails)
                {
                    bagDto.BagHeader.OrderTotal += detail.Product.Price * detail.Count;
                }
            }

            return bagDto;
        }
    }
}
