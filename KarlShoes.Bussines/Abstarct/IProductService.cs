using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.ProductDTOs;

namespace KarlShoes.Bussines.Abstarct
{
    public interface IProductService
    {
        public Task<IResult> ProductAddAsync(ProductAddDTO productAddDTO);
        public IResult ProductRemove(string ProductID);
        public IDataResult<ProductGetDTO> ProductGet(string ProductId, string LangCode);
        public IDataResult<List<ProductGetDTO>> ProductGetAll(string LangCode);
        public Task<IResult> ProductUpdateAsync(ProductUpdateDTO productUpdateDTO);
    }
}
