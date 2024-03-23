using KarlShoes.Core.Utilities.Results.Abtsract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Core.Utilities.Results.Concrete
{
    public class DataResult<T>:Result,IDataResult<T>
    {
        public T Data { get; }
        public DataResult(T data, bool Issuccess, string message,HttpStatusCode statusCode) : base(Issuccess, message,statusCode)
        {
            Data = data;
        }
        public DataResult(T data, bool success,HttpStatusCode statusCode) : base(success,statusCode)
        {
            Data = data;
        }

    }
}
