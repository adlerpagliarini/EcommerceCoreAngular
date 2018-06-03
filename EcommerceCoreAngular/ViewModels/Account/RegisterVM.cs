using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.ViewModels.Account
{
    public class RegisterVM
    {
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
