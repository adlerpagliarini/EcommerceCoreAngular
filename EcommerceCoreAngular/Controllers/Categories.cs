using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceCoreAngular.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCoreAngular.Controllers
{
    [Route("[controller]/[action]")]
    public class Categories : Controller
    {
        private readonly ICategory _categoryRepository;

        public Categories(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var category = _categoryRepository.GetAll().ToList();
            return View(category);
        }
    }
}