using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        public string CategoryId { get; set; }


        public bool IsFeatured { get; set; }
      public Dictionary<string, string> CategoryNames { get; set; }
    }
}
