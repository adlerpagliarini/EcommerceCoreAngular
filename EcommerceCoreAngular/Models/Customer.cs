using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Models
{
    [Table("Customer")]
    public class Customer : IdentityUser
    {
        public string CustomerName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        public DateTime? DateEntered { get; set; }

    }
}
