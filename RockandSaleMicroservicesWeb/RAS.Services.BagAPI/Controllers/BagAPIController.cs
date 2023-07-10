using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAS.MessageBus;
using RAS.Services.BagAPI.Messages;
using RAS.Services.BagAPI.Models.Dto;
using RAS.Services.BagAPI.RabbitMQSender;
using RAS.Services.BagAPI.Repository;

namespace RAS.Services.BagAPI.Controllers
{
    //[ApiController]
    [Route("api/bag")]
    public class BagAPIController : ControllerBase
    {
        private readonly IBagRepository _bagRepository;
        //private readonly IMessageBus _messageBus;
        protected ResponseDto _response;
        private readonly IRabbitMQBagMessageSender _rabbitMQBagMessageSender;
        //IMessageBus messageBus;

        public BagAPIController(IBagRepository bagRepository,/*IMessageBus messageBus,*/ IRabbitMQBagMessageSender rabbitMQBagMessageSender)
        {
            _bagRepository = bagRepository;
            _rabbitMQBagMessageSender = rabbitMQBagMessageSender;
            //_messageBus = messageBus;
            this._response = new ResponseDto();
        }

        //Attribute ile metod ismi aynı olmalı
        [HttpGet("GetBag/{userId}")]
        public async Task<object> GetBag(string userId)
        {
            try
            {
                BagDto bagDto = await _bagRepository.GetBagbyUserId(userId);
                _response.Result = bagDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response; //==> bagdto tipinde bir object response.Result içinde dönecek
        }

        //[HttpPost("AddBag")]
        //public async Task<object> AddBag(BagDto bagDto)
        //{
        //    try
        //    {
        //        BagDto model = await _bagRepository.CreateUpdateBag(bagDto);
        //        _response.Result = model;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //    }
        //    return _response;
        //}

        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] BagDto bagDto)
        {
            try
            {
                BagDto model = await _bagRepository.CreateUpdateBag(bagDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        //      [HttpPost]
        //[Authorize]
        //public async Task<object> AddBag([FromBody] BagDto bagDto)
        //{
        //	try
        //	{
        //		BagDto model = await _bagRepository.CreateUpdateBag(bagDto);
        //		_response.Result = model;
        //	}
        //	catch (Exception ex)
        //	{
        //		_response.IsSuccess = false;
        //		_response.ErrorMessages
        //			 = new List<string>() { ex.ToString() };
        //	}
        //	return _response;
        //}

        [HttpPut("UpdateBag")]
        public async Task<object> UpdateBag(BagDto bagDto)
        {
            try
            {
                BagDto model = await _bagRepository.CreateUpdateBag(bagDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveBag")]
        public async Task<object> RemoveBag([FromBody]int bagId)
        {
            try
            {
                bool isSuccess = await _bagRepository.RemoveFromBag(bagId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        //[HttpPost("Checkout")]
        //public async Task<object> Checkout(CheckoutHeaderDto checkoutHeader)
        //{
        //    try
        //    {
        //        BagDto bagDto = await _bagRepository.GetBagbyUserId(checkoutHeader.UserId);
        //        if (bagDto == null)
        //        {
        //            return BadRequest();
        //        }

        //        checkoutHeader.BagDetails = bagDto.BagDetails;
        //        //logic to add message to process order.
        //         //await _messageBus.PublishMessage(checkoutHeader, "checkoutqueue");

        //        //rabbitMQ
        //        _rabbitMQBagMessageSender.SendMessage(checkoutHeader, "checkoutqueue");
        //        await _bagRepository.ClearBag(bagDto.BagHeader.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //    }
        //    return _response;
        //}

        [HttpPost("Checkout")]
        public async Task<object> Checkout(BagDto bagDto)
        {
            try
            {
                await _bagRepository.ClearBag(bagDto.BagHeader.UserId);
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
