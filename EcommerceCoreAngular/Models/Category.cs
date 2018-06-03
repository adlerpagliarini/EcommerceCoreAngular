using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            SubCategories = new HashSet<SubCategory>();
        }


        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
