using Microsoft.AspNetCore.Http;

namespace KarlShoes.Entites.DTOs.PictureDTOs
{
    public class PictureAddDTO
    {
        public IFormFileCollection FormFiles { get; set; }
        public string ProductId { get; set; }
    }
}
