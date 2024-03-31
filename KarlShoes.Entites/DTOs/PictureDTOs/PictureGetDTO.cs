using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.PictureDTOs
{
    public class PictureGetDTO
    {
        public string PictureId { get; set; }
        public string PictureUrl { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
