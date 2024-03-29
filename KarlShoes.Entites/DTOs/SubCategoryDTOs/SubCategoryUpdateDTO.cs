using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.SubCategoryDTOs
{
    public class SubCategoryUpdateDTO
    {
        public string SubCategoryId { get; set; }
        public Dictionary<string, string>? SubCategoriesName { get; set; }
        public string? NewCategoryId { get; set; }
    }
}
