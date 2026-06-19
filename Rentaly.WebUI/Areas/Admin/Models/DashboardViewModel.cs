using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int UnavailableCars { get; set; }
        public int TotalBranches { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalBrands { get; set; }
        public int TotalCategories { get; set; }
        public int TotalRentals { get; set; }
        public int PendingRentals { get; set; }
        public int ApprovedRentals { get; set; }
        public int RejectedRentals { get; set; }
        public decimal AverageDailyPrice { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<Rental> RecentRentals { get; set; } = new();
        public List<CategoryCount> CarsByCategory { get; set; } = new();
        public List<BranchCount> CarsByBranch { get; set; } = new();
    }

    public class CategoryCount
    {
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }

    public class BranchCount
    {
        public string BranchName { get; set; }
        public int Count { get; set; }
    }
}
