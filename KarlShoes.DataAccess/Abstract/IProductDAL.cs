using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Abstract
{
    public interface IProductDAL
    {
        public Task<IResult> ProductAddAsync(ProductAddDTO productAddDTO,List<IFormFile> formFiles);
        public IResult ProductRemove(string ProductID);
        public IDataResult<ProductGetDTO> ProductGet(string ProductId, string LangCode);
        public IDataResult<List<ProductGetDTO>> ProductGetAll(string LangCode);
        public IResult ProductUpdate(ProductUpdateDTO productUpdateDTO);
    }
}
