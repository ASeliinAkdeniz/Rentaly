using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Models
{
    public class CarListViewModel
    {
        public List<Car> Cars { get; set; } = new();
        public List<Brand> Brands { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Branch> Branches { get; set; } = new();

        public int? SelectedBranchId { get; set; }
        public int? SelectedBrandId { get; set; }
        public int? SelectedCategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
