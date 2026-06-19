using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DTOLayer.CustomerDtos;
using Rentaly.EntityLayer.Entities;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;

        public BookingController(
            ICarService carService,
            IBranchService branchService,
            ICustomerService customerService,
            IRentalService rentalService)
        {
            _carService = carService;
            _branchService = branchService;
            _customerService = customerService;
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int carId, DateTime? pickupDate, DateTime? returnDate)
        {
            var car = await _carService.TGetByIdAsync(carId);
            if (car == null)
                return RedirectToAction("NotFoundPage", "Home");

            var vm = new BookingViewModel
            {
                Car = car,
                Branches = await _branchService.TGetListAsync(),
                PickupDate = pickupDate ?? DateTime.Now.Date.AddDays(1),
                ReturnDate = returnDate ?? DateTime.Now.Date.AddDays(3),
                PickupBranchId = car.BranchId,
                ReturnBranchId = car.BranchId
            };

            return View("Index", vm);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitBooking(BookingFormSubmit form)
        {
            // Temel doğrulama
            if (string.IsNullOrWhiteSpace(form.Name) || string.IsNullOrWhiteSpace(form.Email) ||
                string.IsNullOrWhiteSpace(form.Phone) || form.PickupDate >= form.ReturnDate)
            {
                TempData["BookingError"] = "Lütfen tüm zorunlu alanları doldurun ve tarihleri kontrol edin.";
                return RedirectToAction("Index", new { carId = form.CarId, pickupDate = form.PickupDate, returnDate = form.ReturnDate });
            }

            var car = await _carService.TGetByIdAsync(form.CarId);
            if (car == null)
                return RedirectToAction("NotFoundPage", "Home");

            // Tarih çakışması kontrolü — araç gerçekten müsait mi?
            var allRentals = await _rentalService.TGetListAsync();
            var hasConflict = allRentals.Any(r =>
                r.CarId == form.CarId &&
                r.Status != "Reddedildi" &&
                form.PickupDate < r.ReturnDate &&
                form.ReturnDate > r.PickupDate);

            if (hasConflict)
            {
                TempData["BookingError"] = "Üzgünüz, bu araç seçtiğiniz tarihlerde başka bir müşteri tarafından rezerve edilmiş.";
                return RedirectToAction("Index", new { carId = form.CarId, pickupDate = form.PickupDate, returnDate = form.ReturnDate });
            }

            // Müşteriyi email ile bul, yoksa oluştur (TGetListAsync DTO listesi döndürüyor)
            var customers = await _customerService.TGetListAsync();
            var existingCustomer = customers.FirstOrDefault(c =>
                c.Email != null && c.Email.Trim().ToLower() == form.Email.Trim().ToLower());

            int customerId;
            if (existingCustomer != null)
            {
                customerId = existingCustomer.CustomerId;
            }
            else
            {
                var nameParts = form.Name.Trim().Split(' ', 2);
                var newCustomerDto = new CreateCustomerDto
                {
                    Name = nameParts[0],
                    Surname = nameParts.Length > 1 ? nameParts[1] : "",
                    Email = form.Email.Trim(),
                    Phone = form.Phone.Trim(),
                    IdentityNumber = "",
                    DrivingLicenseNumber = "",
                    DrivingLicenseDate = DateTime.Now
                };

                await _customerService.TInsertAsync(newCustomerDto);

                var refreshed = await _customerService.TGetListAsync();
                customerId = refreshed.First(c => c.Email != null &&
                    c.Email.Trim().ToLower() == newCustomerDto.Email.Trim().ToLower()).CustomerId;
            }

            // Toplam fiyat hesabı
            var days = Math.Max(1, (form.ReturnDate.Date - form.PickupDate.Date).Days);
            var totalPrice = car.DailyPrice * days;

            var rental = new Rental
            {
                CarId = form.CarId,
                CustomerId = customerId,
                PickupBranchId = form.PickupBranchId,
                ReturnBranchId = form.ReturnBranchId,
                PickupDate = form.PickupDate,
                ReturnDate = form.ReturnDate,
                TotalPrice = totalPrice,
                Status = "Beklemede"
            };

            await _rentalService.TInsertAsync(rental);

            return RedirectToAction("Success", new { carId = form.CarId });
        }

        public async Task<IActionResult> Success(int carId)
        {
            var car = await _carService.TGetByIdAsync(carId);
            return View("Success", car);
        }
        [HttpGet]
        public async Task<IActionResult> QuickBooking()
        {
            var allCars = await _carService.TGetAllCarsWithCategoryAsync();
            var vm = new QuickBookingViewModel
            {
                Cars = allCars.Where(c => c.IsActive && c.IsAvailable).ToList(),
                Branches = await _branchService.TGetListAsync(),
                PickupDate = DateTime.Now.Date.AddDays(1),
                ReturnDate = DateTime.Now.Date.AddDays(3)
            };
            return View("QuickBooking", vm);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitQuickBooking(BookingFormSubmit form)
        {
            return await SubmitBooking(form); // Aynı submit logic
        }
    }
}
