﻿using KarlShoes.Core.Utilities.Results.Abtsract;
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
        IDataResult<string> AddOrder(AddOrderDTO orderDTO);
        IResult DeleteOrder(string OrderId);
        IDataResult<GetOrderDTO> GetOrder(string id);
        IDataResult<List<GetOrderDTO>> GetAllOrder();

    }
}
