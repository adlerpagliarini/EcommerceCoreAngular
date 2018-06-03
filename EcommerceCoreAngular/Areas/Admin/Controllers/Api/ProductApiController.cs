using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceCoreAngular.Services.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.Areas.Admin.AdminVM;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceCoreAngular.Areas.Admin.Controllers.Api
{
    [Area("Admin")]
    [Route("[area]/api/ProductApi")]
    public class ProductApiController : Controller
    {
        #region Constructure
        private readonly IProduct _productRepository;
        private readonly ICategory _categoryRepository;
        private IHostingEnvironment _environment;
        private readonly UserManager<Customer> _userManager;

        public ProductApiController(IProduct productRepository, ICategory categoryRepository, IHostingEnvironment environment, UserManager<Customer> userManager)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _environment = environment;
            _userManager = userManager;
        } 
        #endregion

        #region Get
        // GET: api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var product = _productRepository.GetAll();
            return product;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // GET api/values/5
        [HttpGet("VM/{id}")]
        public IActionResult GetProductVMForUpdate([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var prod = new UpdateProductVM

            {
                Products = _productRepository.GetById(id),
                Categories = _categoryRepository.GetAll()
            };
            return Ok(prod);
        }

        [HttpGet("VM")]
        public IActionResult GetProductVMForInsert()
        {
            var prod = new CreateProductVM
            {
                Products = new Product(),
                Categories = _categoryRepository.GetAll().ToList()
            };

            return Ok(prod);
        }

        #endregion

        #region Create
        [HttpPost("UploadFiles")]
        public IActionResult UploadFiles(List<IFormFile> files)
        {
            IActionResult result = null;

            long size = files.Sum(f => f.Length);

            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                }
            }

            return Ok(new { count = files.Count, size, filePath });
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromForm]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product.ProductImage !=null && product.ProductImage.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");

                using (var fileStream = new FileStream(Path.Combine(uploads, product.ProductImage.FileName), FileMode.Create))
                {
                    product.ProductImage.CopyTo(fileStream);
                }

                product.ProductImagePath = product.ProductImage.FileName.ToString();
            }

            _productRepository.Insert(product);
            try
            {
                _productRepository.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
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

            if (!ProductExists(id))
            {
                return NotFound();
            }

            _productRepository.Delete(id);
            _productRepository.Save();
            TempData.Add("result", "Product Deleted");

            return Ok();
        }
        #endregion

        private bool ProductExists(int id)
        {
            return _productRepository.GetById(id) != null;
        }

    }
}
