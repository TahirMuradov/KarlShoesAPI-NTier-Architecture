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

        public IDataResult<string> AddOrder(AddOrderDTO orderDTO)
        {
            return _orderDAL.AddOrder(orderDTO);
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
