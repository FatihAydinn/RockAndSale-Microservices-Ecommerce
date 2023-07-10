using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAS.Services.BagAPI.Messages;
using RAS.Services.BagAPI.Models.Dto;
using RAS.Services.BagAPI.RabbitMQSender;
using RAS.Services.BagAPI.Repository;

namespace RAS.Services.BagAPI.Controllers
{
    [Route("api/checkout")]
    public class BagAPICheckOutController : ControllerBase
    {

        private readonly IBagRepository _bagRepository;
        // private readonly IMessageBus _messageBus;
        protected ResponseDto _response;
        private readonly IRabbitMQBagMessageSender _rabbitMQBagMessageSender;
        // IMessageBus messageBus,
        public BagAPICheckOutController(IBagRepository bagRepository,
             IRabbitMQBagMessageSender rabbitMQBagMessageSender)
        {
            _bagRepository = bagRepository;
            _rabbitMQBagMessageSender = rabbitMQBagMessageSender;
            //_messageBus = messageBus;
            this._response = new ResponseDto();
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Checkout([FromBody] CheckoutHeaderDto checkoutHeader)
        {
            //GetBagByUserId
            try
            {
                BagDto bagDto = await _bagRepository.GetBagbyUserId(checkoutHeader.UserId);
                if (bagDto == null)
                {
                    return BadRequest();
                }

                checkoutHeader.BagDetails = bagDto.BagDetails;
                //logic to add message to process order.
                // await _messageBus.PublishMessage(checkoutHeader, "checkoutqueue");

                ////rabbitMQ
                _rabbitMQBagMessageSender.SendMessage(checkoutHeader, "checkoutqueue");
                await _bagRepository.ClearBag(checkoutHeader.UserId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
