﻿using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class SubCategory:IEntity
    {
        public Guid Id { get; set; }
        public List<SubCategoryLaunguage> subCategoryLaunguages {  get; set; }
        public List<SubCategoryProduct> SubCategoryProducts { get; set; }
        public Guid CategoryId { get; set; }   
        public Category Category { get; set; }

    }
}
