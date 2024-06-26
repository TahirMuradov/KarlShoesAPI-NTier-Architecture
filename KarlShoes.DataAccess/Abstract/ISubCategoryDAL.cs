﻿using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Abstract
{
    public interface ISubCategoryDAL
    {

        public IResult SubCategoryDelete(string id);
        public IDataResult<SubCategoryGetDTO> SubCategoryGet(string id,string LangCode);
        public IDataResult<List<SubCategoryGetDTO>>SubCategoryAllGet(string LangCode);
        public IResult SubCategoryAdd(SubCategoryAddDTO subCategoryAddDTO);
        public IResult SubCategoryUpdate (SubCategoryUpdateDTO subCategoryUpdateDTO);
      
    }
}
