using KarlShoes.Core.Utilities.Results.Abtsract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Abstract
{
    public interface IProductDAL
    {
        public IResult ProductAdd();
    }
}
