using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Core.Utilities.Results.Abtsract
{
    public interface IResult
    {
        public bool IsSuccess { get; }
        public HttpStatusCode StatusCode {get;}
        public string Message { get; }
    }
}
