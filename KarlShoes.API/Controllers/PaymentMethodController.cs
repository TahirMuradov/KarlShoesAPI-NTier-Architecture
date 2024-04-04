using Entities.DTOs.PaymentMethodDTOs;
using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.PaymentMethodDTOValidator;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [HttpGet("[action]")]
        public IActionResult GetPaymentMethod(string id, string langCode)
        {
            if (string.IsNullOrEmpty(langCode) || string.IsNullOrEmpty(id)) return BadRequest("Id or LangCode is Null or Empty!");
            var result = _paymentMethodService.GetPaymentMethod(Id: id, langCode: langCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllPaymentMethod(string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode) ) return BadRequest("LangCode is Null or Empty!");
            var resutl=_paymentMethodService.GetAllPaymentMethod(LangCode);
            return resutl.IsSuccess?Ok(resutl) : BadRequest(resutl);

        }
        [HttpDelete("[action]")]
        public IActionResult PaymentMehthodDelete(string Id)
        {
            if ( string.IsNullOrEmpty(Id)) return BadRequest("Id is Null or Empty!");
            var result=_paymentMethodService.DeletePaymentMethod(Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [HttpPost("[action]")]
        public IActionResult AddPaymentMethod(AddPaymentMethodsDTO addPaymentMethodsDTO)
        {
            var validator = new PaymentMethodAddValidator();
            var ResultValidator= validator.Validate(addPaymentMethodsDTO);  
            if (!ResultValidator.IsValid) return BadRequest(ResultValidator.Errors);
            var result=_paymentMethodService.AddPaymentMethod(addPaymentMethodsDTO);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
    }
}
