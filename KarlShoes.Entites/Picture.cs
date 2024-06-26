﻿using KarlShoes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites
{
    public class Picture:IEntity
    {
        public Guid Id { get; set; }
        public string url { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
