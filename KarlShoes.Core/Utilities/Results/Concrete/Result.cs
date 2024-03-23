﻿using KarlShoes.Core.Utilities.Results.Abtsract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Core.Utilities.Results.Concrete
{
    public class Result:IResult
    {
        public bool IsSuccess { get; }

        public string Message { get; }
        public HttpStatusCode StatusCode { get; }   
        public Result(bool IsSuccess ,HttpStatusCode statusCode)
        {
            this.IsSuccess = IsSuccess;
            StatusCode = statusCode;
        }
        public Result(bool IsSuccess, string message,HttpStatusCode statusCode) : this(IsSuccess,statusCode)
        {
            Message = message;
          
        }
    }
}
