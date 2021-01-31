using AutoMapper;
using CMS_RestAPI.DataAccessLayer.Context;
using CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.EntityTypeRepositories;
using CMS_RestAPI.EntityLayer.Entities.Concrete;
using CMS_RestAPI.EntityLayer.Enum;
using CMS_RestAPI.UI.Models.DTOs;
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
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            await _categoryrepo.GetById(id);

            if (_categoryrepo == null)
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
        [HttpGet("{categorySlug}", Name = "GetCategoryBySlug")]
        public async Task<ActionResult<Category>> GetCategoryBySlug(string categorySlug)
        {
            await _categoryrepo.FirstOrDefault(x => x.Slug == categorySlug);
            if (_categoryrepo == null)
            {
                return NotFound();
            }
            return Ok(_categoryrepo);
        }


        /// <summary>
        /// This method help to you insert category into database
        /// </summary>
        /// <param name="category">
        ///     name* string
        ///     minLength: 2
        ///     pattern: ^[a-zA-Z]+$
        ///     slug* string
        ///     createDate*  string ($date-time)
        ///     status Statusinteger($int32)
        ///     Enum:[ 1, 2, 3 ]
        /// </param>
        /// <returns></returns>
        [HttpPost] // Eklemek için kullanılır
        public async Task<ActionResult<Category>> PostCategory(CategoryDTO categoryDTO)
        {
            var categoryObject = _mapper.Map<Category>(categoryDTO);
            await _categoryrepo.Add(categoryObject);
            return CreatedAtAction(nameof(GetCategories), categoryDTO);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "PutCategory")] //Güncellemek için kullanılır
        public async Task<ActionResult<Category>> PutCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest();
            }

            var categoryObject = _mapper.Map<Category>(categoryDTO);

            await _categoryrepo.Update(categoryObject);

            return CreatedAtAction(nameof(GetCategories), categoryDTO);
        }

        [HttpDelete("{int:id}", Name = "DeleteCategory")]
        public async Task<ActionResult<Category>> DeleteCategory(int id, CategoryDTO categoryDTO)
        {
            await _categoryrepo.FindByDefault(x => x.Id == id);

            if (_categoryrepo == null)
            {
                NotFound();
            }

            var categoryObject = _mapper.Map<Category>(categoryDTO);
            await _categoryrepo.Delete(categoryObject);
            return NoContent(); // => Status code 204 dönüyor.
        }
    }
}
