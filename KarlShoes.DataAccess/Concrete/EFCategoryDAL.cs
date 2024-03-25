﻿using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFCategoryDAL : ICategoryDAL
    {
		public IDataResult<List<CategoryGetDTO>> GetAllCategory(string LangCode)
		{
			try
			{
				using var context = new AppDBContext();
				
					var category = context.Categories
						.Include(x => x.CategoryLanguages)

						.Include(x => x.SubCategory)
					 
						.Select(x=>new CategoryGetDTO { 
							Id = x.Id,
							CategoryName=x.CategoryLanguages.FirstOrDefault(x=>x.LangCode==LangCode).CategoryName,
							SubCategoryName=x.SubCategory!=null? x.SubCategory.Where(x=>x.LangCode==LangCode).Select(x=>x.SubcategoryName).ToList():null,
							
						
						
						})
						.ToList();

				return new SuccessDataResult<List<CategoryGetDTO>>(data: category, HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				return new ErrorDataResult<List<CategoryGetDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
			}
		}
        public async Task<IResult> AddCategoryAsync(CategoryAddDTO categoryAddDTO)
        {
			try
			{

				using (var context = new AppDBContext())
				{
					Category category = new Category()
					{
						IsFeatured = categoryAddDTO.IsFeatured,
						CreatedDate=DateTime.Now,
					};
					context.Categories.Add(category);
					context.SaveChanges();
					for (int i=0;i<categoryAddDTO.LangCode.Count;i++)
					{

						CategoryLanguage categoryLanguage = new CategoryLanguage()
						{
							CategoryId = category.Id,
							LangCode = categoryAddDTO.LangCode[i],
							CategoryName = categoryAddDTO.CategoryName[i]

						};
						context.CategoryLanguages.Add(categoryLanguage);
					}
					context.SaveChanges();
				}
					return new SuccessResult(statusCode: HttpStatusCode.OK);
			}
			catch (Exception ex)
			{

				return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
			}
        }

        public IResult DeleteCategory(string id)
        {
			try
			{

				using (var context =new AppDBContext())
				{
					var category = context.Categories.FirstOrDefault(x => x.Id.ToString() == id);

					var categoryLaunge = context.CategoryLanguages.Where(x => x.CategoryId == category.Id);
					context.RemoveRange(categoryLaunge);
					var categoryProduct = context.CategoryProducts.Where(x => x.CategoryId == category.Id);
					context.RemoveRange(categoryProduct);
					var categorySubCategory = context.subCategories.Where(x => x.CategoryId == category.Id);
					context.subCategories.RemoveRange(categorySubCategory);
					context.Categories.Remove(category);
					context.SaveChanges();
				}
				return new SuccessResult(statusCode: HttpStatusCode.OK);
			}
			catch (Exception ex)
			{

				return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
			}
        }

        public IDataResult<CategoryGetDTO> GetCategory(string id,string LangCode)
        {
			try
			{
				using (var context = new AppDBContext())
				{
					var category = context.Categories
						.Include(x => x.CategoryLanguages.Where(x=>x.LangCode==LangCode))
						
						.Include(x => x.SubCategory.Where(x => x.LangCode == LangCode))
						.FirstOrDefault(x=>x.Id.ToString()==id);
					return new SuccessDataResult<CategoryGetDTO>(new CategoryGetDTO
					{
						CategoryName = category.CategoryLanguages.FirstOrDefault(x=>x.LangCode==LangCode).CategoryName,

					},statusCode:HttpStatusCode.OK);;

				}
			}
			catch (Exception ex)
			{

				return new ErrorDataResult<CategoryGetDTO>(message:ex.Message,statusCode: HttpStatusCode.BadRequest);
			}
        }

        public  IResult UpdateCatgeory(CategoryUpdateDTO categoryUpdateDTO)
        {
			try
			{
				using (var context = new AppDBContext())
				{
					var category = context.Categories.FirstOrDefault(x => x.Id.ToString() == categoryUpdateDTO.CategoryId);
					var categoryLaunguages = context.CategoryLanguages.Where(x => x.CategoryId.ToString() == categoryUpdateDTO.CategoryId);
					category.IsFeatured=categoryUpdateDTO.IsFeatured;
					context.Categories.Update(category);

                    foreach (var categoryLaunguage in categoryLaunguages)
                    {
                        if (categoryUpdateDTO.CategoryNames.ContainsKey(categoryLaunguage.LangCode))
                        {
							categoryLaunguage.CategoryName = categoryUpdateDTO.CategoryNames[categoryLaunguage.LangCode];
                        }


                    }
					context.CategoryLanguages.UpdateRange(categoryLaunguages);

			
					
				
					context.SaveChanges();

                }
				return new SuccessResult(statusCode: HttpStatusCode.OK);
			}
			catch (Exception ex)
			{

				return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
			}
        }
    }
}
