using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.WebUI.Areas.Admin.Models;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public DashboardController(
            ICarService carService,
            IBranchService branchService,
            ICustomerService customerService,
            IRentalService rentalService,
            IBrandService brandService,
            ICategoryService categoryService)
        {
            _carService = carService;
            _branchService = branchService;
            _customerService = customerService;
            _rentalService = rentalService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Dashboard";
            ViewData["ActiveMenu"] = "Dashboard";

            var cars = await _carService.TGetListAsync();
            var branches = await _branchService.TGetListAsync();
            var customers = await _customerService.TGetListAsync();
            var rentals = await _rentalService.TGetListAsync();
            var brands = await _brandService.TGetListAsync();
            var categories = await _categoryService.TGetListAsync();

            var vm = new DashboardViewModel
            {
                TotalCars = cars.Count,
                AvailableCars = cars.Count(c => c.IsAvailable),
                UnavailableCars = cars.Count(c => !c.IsAvailable),
                TotalBranches = branches.Count,
                TotalCustomers = customers.Count,
                TotalBrands = brands.Count,
                TotalCategories = categories.Count,
                TotalRentals = rentals.Count,
                PendingRentals = rentals.Count(r => r.Status == "Beklemede"),
                ApprovedRentals = rentals.Count(r => r.Status == "Onaylandı"),
                RejectedRentals = rentals.Count(r => r.Status == "Reddedildi"),
                AverageDailyPrice = cars.Any() ? cars.Average(c => c.DailyPrice) : 0,
                TotalRevenue = rentals.Where(r => r.Status == "Onaylandı").Sum(r => r.TotalPrice),
                RecentRentals = rentals.OrderByDescending(r => r.RentalId).Take(5).ToList(),
                CarsByCategory = cars
                    .GroupBy(c => c.Category?.CategoryName ?? "Diğer")
                    .Select(g => new CategoryCount { CategoryName = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .ToList(),
                CarsByBranch = cars
                    .GroupBy(c => c.Branch?.BranchName ?? "Diğer")
                    .Select(g => new BranchCount { BranchName = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .ToList()
            };

            return View(vm);
        }
    }
}
