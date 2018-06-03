using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceCoreAngular.Models
{
    [Table("Picture")]
    public class Picture
    {
        public int PictureId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }
    }
}
