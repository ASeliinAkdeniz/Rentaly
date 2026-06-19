using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Models
{
    public class HomeIndexViewModel
    {
        public List<Process> Processes { get; set; } = new();
        public List<OurFuture> OurFutures { get; set; } = new();
        public List<Statistic> Statistics { get; set; } = new();
        public List<Award> Awards { get; set; } = new();
        public List<Testimonial> Testimonials { get; set; } = new();
        public List<FAQ> FAQs { get; set; } = new();
        public List<Brand> Brands { get; set; } = new();
        public List<Branch> Branches { get; set; } = new();
        public List<Car> FeaturedCars { get; set; } = new();
    }
}
