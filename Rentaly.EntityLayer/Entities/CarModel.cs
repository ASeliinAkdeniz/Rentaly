using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class CarModel
    {
        [Key]
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }

    }
}
