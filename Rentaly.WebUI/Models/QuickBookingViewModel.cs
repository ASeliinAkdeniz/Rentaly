using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Models
{
    public class QuickBookingViewModel
    {
        public List<Car> Cars { get; set; } = new();
        public List<Branch> Branches { get; set; } = new();
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
