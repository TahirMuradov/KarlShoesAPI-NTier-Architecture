using KarlShoes.Core.Utilities.Results.Abtsract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Abstract
{
    public interface IOrderDAL
    {
       public IResult AddOrder();
        public IResult RemoveOrder();
        public IResult GetOrder(int Orderid);

    }
}
