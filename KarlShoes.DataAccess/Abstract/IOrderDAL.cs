using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Abstract
{
    public interface IOrderDAL
    {
       public Task< IDataResult<string>> AddOrderAsync(AddOrderDTO orderDTO);
        public IResult DeleteOrder(string OrderId);
        public IDataResult<GetOrderDTO> GetOrder(string id);
        public IDataResult<List<GetOrderDTO>> GetAllOrder();

    }
}
