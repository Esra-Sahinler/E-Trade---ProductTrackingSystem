using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackingSystem.Entities.Concrete
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }

        public string ColorName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
