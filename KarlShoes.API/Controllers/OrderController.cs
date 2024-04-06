using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.OrderDTOValidator;
using KarlShoes.Entites.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("[action]")]
        public IActionResult GetOrder(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id is Null Or Empty");
            var result= _orderService.GetOrder(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [HttpGet("[action]")]
        public IActionResult GetAllOrder()
        {
            var result=_orderService.GetAllOrder();
            return result.IsSuccess? Ok(result) : BadRequest(result) ;

        }
        [HttpDelete("[action]")]
        public IActionResult DeleteOrder(string id)
        {
            if(string.IsNullOrEmpty(id)) return BadRequest("Id is Null Or Empty");
            var result=_orderService.DeleteOrder(id);
            return result.IsSuccess?Ok(result) : BadRequest(result) ;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrder(AddOrderDTO addOrderDTO)
        {
            var validator = new AddOrderDTOValidator();
            var ResultValidator= validator.Validate(addOrderDTO);   
            if (!ResultValidator.IsValid) return BadRequest(ResultValidator.Errors);
            var result=await _orderService.AddOrderAsync(addOrderDTO);
            return result.IsSuccess?Ok(result):BadRequest(result) ;
        }
    }
}
