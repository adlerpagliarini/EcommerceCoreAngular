using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Models
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customers { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
