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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{int:id}",Name ="GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            await _categoryrepo.GetById(id);

            if (_categoryrepo==null)
            {
                return NotFound();
            }
            return Ok(_categoryrepo);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categorySlug"></param>
        /// <returns></returns>
        [HttpGet("{categorySlug}",Name ="GetCategoryBySlug")]
        public async Task<ActionResult<Category>> GetCategoryBySlug(string categorySlug)
        {
            await _categoryrepo.FirstOrDefault(x => x.Slug == categorySlug);
            if (_categoryrepo == null)
            {
                return NotFound();
            }
            return Ok(_categoryrepo);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoryrepo.Add(category);
            await _categoryrepo.Save();
            return CreatedAtAction(nameof(GetCategories), category);
        }
    }
}
