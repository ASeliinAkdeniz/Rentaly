using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly ICarModelService _carModelService;
        private readonly ICategoryService _categoryService;
        private readonly IBranchService _branchService;
        private readonly IRentalService _rentalService;

        public CarController(
            ICarService carService,
            IBrandService brandService,
            ICarModelService carModelService,
            ICategoryService categoryService,
            IBranchService branchService,
            IRentalService rentalService)
        {
            _carService = carService;
            _brandService = brandService;
            _carModelService = carModelService;
            _categoryService = categoryService;
            _branchService = branchService;
            _rentalService = rentalService;
        }

        public async Task<IActionResult> CarList(int? branchId, int? brandId, int? categoryId,
            decimal? minPrice, decimal? maxPrice, DateTime? pickupDate, DateTime? returnDate)
        {
            var allCars = await _carService.TGetAllCarsWithCategoryAsync();
            var rentals = await _rentalService.TGetListAsync();

            var query = allCars.Where(c => c.IsActive).AsQueryable();

            // Lokasyon filtresi
            if (branchId.HasValue)
                query = query.Where(c => c.BranchId == branchId.Value);

            // Marka filtresi
            if (brandId.HasValue)
                query = query.Where(c => c.BrandId == brandId.Value);

            // Kategori filtresi
            if (categoryId.HasValue)
                query = query.Where(c => c.CategoryId == categoryId.Value);

            // Fiyat aralığı filtresi
            if (minPrice.HasValue)
                query = query.Where(c => c.DailyPrice >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(c => c.DailyPrice <= maxPrice.Value);

            var cars = query.ToList();

            // Tarih bazlı müsaitlik kontrolü:
            // Seçilen tarihlerle çakışan onaylanmış/bekleyen rezervasyonu olan araçlar listeden çıkarılır
            if (pickupDate.HasValue && returnDate.HasValue)
            {
                var busyCarIds = rentals
                    .Where(r => r.Status != "Reddedildi" &&
                                pickupDate.Value < r.ReturnDate &&
                                returnDate.Value > r.PickupDate)
                    .Select(r => r.CarId)
                    .ToHashSet();

                cars = cars.Where(c => !busyCarIds.Contains(c.CarId) && c.IsAvailable).ToList();
            }
            else
            {
                cars = cars.Where(c => c.IsAvailable).ToList();
            }

            var vm = new CarListViewModel
            {
                Cars = cars,
                Brands = await _brandService.TGetListAsync(),
                Categories = await _categoryService.TGetListAsync(),
                Branches = await _branchService.TGetListAsync(),
                SelectedBranchId = branchId,
                SelectedBrandId = brandId,
                SelectedCategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                PickupDate = pickupDate,
                ReturnDate = returnDate
            };

            return View("CarList", vm);
        }

        public async Task<IActionResult> CarDetail(int id)
        {
            var car = await _carService.TGetByIdAsync(id);
            if (car == null)
                return RedirectToAction("NotFoundPage", "Home");

            return View("CarDetail", car);
        }
    }
}
