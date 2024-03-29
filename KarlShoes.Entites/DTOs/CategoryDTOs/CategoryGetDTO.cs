using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.CategoryDTOs
{
    public class CategoryGetDTO
    {
        public Guid Id { get; set; }
        public string  CategoryName { get; set; }
        public bool IsFetured { get; set; }
        public List<string>? SubCategoryName { get; set; }


    }
}
