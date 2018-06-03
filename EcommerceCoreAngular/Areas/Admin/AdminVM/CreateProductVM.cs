using EcommerceCoreAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Areas.Admin.AdminVM
{
    public class CreateProductVM
    {
        public Product Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
