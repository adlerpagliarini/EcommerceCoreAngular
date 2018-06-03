using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceCoreAngular.Services.Infrastructure;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceCoreAngular.Areas.Admin.Controllers.Api
{
    [Area("Admin")]
    [Route("[area]/api/CategoryApi")]
    public class CategoryApiController : Controller
    {
        private readonly ICategory _categoryRepository;

        public CategoryApiController(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #region Get
        [HttpGet]
        public IEnumerable<Models.Category> Get()
        {
            var categories = _categoryRepository.GetAll();

            return categories;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get([FromBody] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Models.Category category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        #endregion

        #region Post
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Models.Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _categoryRepository.Insert(category);
                _categoryRepository.Save();
            }
            catch (Exception ex)
            {
                if (CategoryExist(category.CategoryId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }

            }

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }
        #endregion

        #region Update
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody]Models.Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            try
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
            }
            catch (Exception ex)
            {
                if (!CategoryExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok(category);
        }
        #endregion

        #region Delete
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CategoryExist(id))
            {
                return NotFound();
            }

            _categoryRepository.Delete(id);
            _categoryRepository.Save();

            return Ok();
        } 
        #endregion

        private bool CategoryExist(int id)
        {
            return _categoryRepository.GetById(id) != null;
        }
    }
}
