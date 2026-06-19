using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Models
{
    public class BookingViewModel
    {
        public Car Car { get; set; }
        public List<Branch> Branches { get; set; } = new();
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int PickupBranchId { get; set; }
        public int ReturnBranchId { get; set; }
    }

    public class BookingFormSubmit
    {
        public int CarId { get; set; }
        public int PickupBranchId { get; set; }
        public int ReturnBranchId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
