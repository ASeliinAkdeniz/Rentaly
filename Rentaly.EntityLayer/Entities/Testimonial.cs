using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    // Testimonial.cs
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public int Rating { get; set; }
    }
}
