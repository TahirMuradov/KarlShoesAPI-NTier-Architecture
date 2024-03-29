using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.CategoryDTOs
{
    public class CategoryAddDTO
    {

        public string CreatorUserId { get; set; }
        public bool IsFeatured { get; set; }
        public List<string> CategoryName { get; set; }
        public List<string> LangCode { get; set; }
    }
}
