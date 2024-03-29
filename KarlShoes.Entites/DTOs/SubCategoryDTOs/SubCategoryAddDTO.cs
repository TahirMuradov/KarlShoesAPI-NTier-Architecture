using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.SubCategoryDTOs
{
    public class SubCategoryAddDTO
    {
        public Guid CategoryId { get; set; }
        public Dictionary<string, string> SubCategoryName { get; set; }

    }
}
