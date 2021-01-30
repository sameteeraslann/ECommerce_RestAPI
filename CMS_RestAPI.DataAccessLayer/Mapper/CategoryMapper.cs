using AutoMapper;
using CMS_RestAPI.EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_RestAPI.DataAccessLayer.Mapper
{
    public class CategoryMapper:Profile
    {
        public CategoryMapper() => CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}
