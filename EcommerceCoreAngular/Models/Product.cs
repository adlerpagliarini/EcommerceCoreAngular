using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Models
{
    [Table("Product")]
    public class Product
    {

        public int ProductId { get; set; }

        [Required, Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public string Details { get; set; }

        public decimal UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        public IFormFile ProductImage { get; set; }

        public string ProductImagePath { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Categories { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public int? PictureId { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

        public Product()
        {
            CartItems = new HashSet<CartItem>();
            OrderLines = new HashSet<OrderLine>();

        }

    }
}
