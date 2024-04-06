using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.Entites.DTOs.OrderDTOs;

namespace KarlShoes.Bussines.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;

        public OrderManager(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }

        public async Task<IDataResult<string>> AddOrderAsync(AddOrderDTO orderDTO)
        {
            return await _orderDAL.AddOrderAsync(orderDTO);
        }

        public IResult DeleteOrder(string OrderId)
        {
            return _orderDAL.DeleteOrder(OrderId);
        }

        public IDataResult<List<GetOrderDTO>> GetAllOrder()
        {
            return _orderDAL.GetAllOrder();
        }

        public IDataResult<GetOrderDTO> GetOrder(string id)
        {
            return _orderDAL.GetOrder(id);
        }
    }
}
