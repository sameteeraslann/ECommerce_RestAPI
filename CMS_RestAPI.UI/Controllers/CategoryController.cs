using AutoMapper;
using CMS_RestAPI.DataAccessLayer.Context;
using CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.EntityTypeRepositories;
using CMS_RestAPI.EntityLayer.Entities.Concrete;
using CMS_RestAPI.EntityLayer.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_RestAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       
        private readonly ICategoryRepository _categoryrepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repo,
                                  IMapper mapper)
        {
            _categoryrepo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories() => await _categoryrepo.Get(x => x.Status != Status.Passive);
        
        [HttpGet("{int:id}",Name ="GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryById(int id) => await _categoryrepo.GetById(id);
        
    }
}
