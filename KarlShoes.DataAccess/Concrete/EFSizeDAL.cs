using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.SizeDTOs;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFSizeDAL : ISizeDAL
    {
        private readonly AppDBContext _context;

        public EFSizeDAL(AppDBContext context)
        {
            _context = context;
        }

        public IResult SizeAdd(SizeAddDTO sizeAddDTO)
        {
            try
            {
           
                    var checkedSize = _context.Sizes.FirstOrDefault(x => x.NumberSize == sizeAddDTO.SizeNumber);
                    if (checkedSize != null) new ErrorResult(message: "Data Is Not Empty", HttpStatusCode.BadRequest);
                    Size size = new Size()
                    {
                        CreatedDate = DateTime.Now,
                        NumberSize = sizeAddDTO.SizeNumber,

                    };
                    _context.Sizes.Add(size);
                    _context.SaveChanges();

                
                return new SuccessResult(statusCode: HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorResult(statusCode: HttpStatusCode.BadRequest, message: ex.Message);
            }
        }

        public IDataResult<List<SizeGetDTO>> SizeAllGet()
        {
            try
            {
           

                    return new SuccessDataResult<List<SizeGetDTO>>(
                        data:
                        _context.Sizes.Select(x => new SizeGetDTO
                        {
                            Id = x.Id.ToString(),
                            Size = x.NumberSize
                        }).ToList(),
                        statusCode: HttpStatusCode.OK
                                              );

              
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<SizeGetDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<SizeGetDTO> SizeGet(string SizeId)
        {
            try
            {

                var checkedSizeId = _context.Sizes.FirstOrDefault(x => x.Id.ToString().ToLower() == SizeId.ToLower());
                if (checkedSizeId == null) return new ErrorDataResult<SizeGetDTO>(message: "There is no data with this ID", statusCode: HttpStatusCode.NotFound);

                return new SuccessDataResult<SizeGetDTO>(data: new SizeGetDTO { Id=checkedSizeId.Id.ToString(),Size=checkedSizeId.NumberSize}, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<SizeGetDTO>(message:ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SizeRemove(string SizeId)
        {
            try
            {
                
                    var checkedSize = _context.Sizes.FirstOrDefault(x => x.Id.ToString().ToLower() == SizeId.ToLower());
                    if (checkedSize == null)
                        return new ErrorResult(message: "Size Is Notfound", statusCode: HttpStatusCode.NotFound);
                    _context.Sizes.Remove(checkedSize);
                    _context.ProductSizes.RemoveRange(_context.ProductSizes.Where(x => x.SizeId == checkedSize.Id));
                    _context.SaveChanges();

               
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SizeUpdate(SizeUpdateDTO sizeUpdateDTO)
        {
            try
            {
               
                    var checkedSize = _context.Sizes.FirstOrDefault(x=>x.Id.ToString().ToLower()==sizeUpdateDTO.Id.ToLower());
                    if(checkedSize == null) return new ErrorResult(message: "Size Is Notfound", statusCode: HttpStatusCode.NotFound);
                    checkedSize.NumberSize = sizeUpdateDTO.NewSizeNumber;
                    _context.Sizes.Update(checkedSize);
                    _context.SaveChanges();

            
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult( message:ex.Message,statusCode: HttpStatusCode.BadRequest);
            };
        }
    }
}
