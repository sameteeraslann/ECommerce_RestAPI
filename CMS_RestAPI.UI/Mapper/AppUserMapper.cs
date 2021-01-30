using AutoMapper;
using CMS_RestAPI.EntityLayer.Entities.Concrete;
using CMS_RestAPI.UI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_RestAPI.UI.Mapper
{
    public class AppUserMapper :Profile
    {
        public AppUserMapper() => CreateMap<AppUser, AppUserDTO>().ReverseMap();
        {

        }
    }
}
