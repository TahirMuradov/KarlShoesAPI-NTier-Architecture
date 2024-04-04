using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.Entites.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;

namespace KarlShoes.Bussines.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public async Task<Core.Utilities.Results.Abtsract.IResult> ProductAddAsync(ProductAddDTO productAddDTO)
        {
          return await _productDAL.ProductAddAsync(productAddDTO);
        }

        public IDataResult<ProductGetDTO> ProductGet(string ProductId, string LangCode)
        {
            return _productDAL.ProductGet(ProductId, LangCode);
        }

        public IDataResult<List<ProductGetDTO>> ProductGetAll(string LangCode)
        {
         return _productDAL.ProductGetAll(LangCode);
        }

        public Core.Utilities.Results.Abtsract.IResult ProductRemove(string ProductID)
        {
           return _productDAL.ProductRemove(ProductID);
        }

        public async Task<Core.Utilities.Results.Abtsract.IResult> ProductUpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            return await _productDAL.ProductUpdateAsync(productUpdateDTO);
        }
    }
}
