using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceCoreAngular.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCoreAngular.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class CategoryNG : Controller
    {
        private readonly ICategory _categoryRepository;



        public CategoryNG(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #region Get
        public Models.Category Get(int id)
        {
            var category = _categoryRepository.GetById(id);
            return category;
        }
        #endregion

        #region GetAll
        public IEnumerable<Models.Category> GetAll()
        {
            var categories = _categoryRepository.GetAll().ToList();
            return categories;
        }
        #endregion

        #region CreateNG
        [HttpPost]
        public IActionResult Create([FromBody] Models.Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _categoryRepository.Insert(category);
            _categoryRepository.Save();
            return Ok(category);
        }
        #endregion

        #region UpdateNG
        [HttpPut]
        public IActionResult Update([FromBody] Models.Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _categoryRepository.Update(category);
            _categoryRepository.Save();
            return NoContent();
        }
        #endregion

        #region DeleteNG
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Delete(id);
            _categoryRepository.Save();
            return Ok(category);
        }
        #endregion

    }
}