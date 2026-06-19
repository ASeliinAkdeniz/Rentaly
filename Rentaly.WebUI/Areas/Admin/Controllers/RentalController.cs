using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.EmailService;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;
        private readonly IBranchService _branchService;
        private readonly IEmailService _emailService;

        public RentalController(
            IRentalService rentalService,
            ICustomerService customerService,
            ICarService carService,
            IBranchService branchService,
            IEmailService emailService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _carService = carService;
            _branchService = branchService;
            _emailService = emailService;
        }

        public async Task<IActionResult> RentalList()
        {
            ViewData["Title"] = "Rezervasyonlar";
            ViewData["ActiveMenu"] = "Rental";
            ViewData["BreadcrumbParent"] = "Operasyon";

            var rentals = await _rentalService.TGetListAsync();
            var customers = await _customerService.TGetListAsync();
            var cars = await _carService.TGetAllCarsWithCategoryAsync();
            var branches = await _branchService.TGetListAsync();

            ViewBag.Customers = customers;
            ViewBag.Cars = cars;
            ViewBag.Branches = branches;

            return View("RentalList", rentals.OrderByDescending(r => r.RentalId).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRental(int id)
        {
            var rental = await _rentalService.TGetByIdAsync(id);
            if (rental == null)
            {
                TempData["Error"] = "Rezervasyon bulunamadı.";
                return RedirectToAction("RentalList");
            }

            rental.Status = "Onaylandı";
            await _rentalService.TUpdateAsync(rental);

            // Mail için gerekli verileri topla
            var customer = await _customerService.TGetByIdAsync(rental.CustomerId);
            var cars = await _carService.TGetAllCarsWithCategoryAsync();
            var car = cars.FirstOrDefault(c => c.CarId == rental.CarId);
            var branches = await _branchService.TGetListAsync();
            var pickupBranch = branches.FirstOrDefault(b => b.BranchId == rental.PickupBranchId);
            var returnBranch = branches.FirstOrDefault(b => b.BranchId == rental.ReturnBranchId);

            if (customer != null && !string.IsNullOrEmpty(customer.Email))
            {
                var carInfo = car != null
                    ? $"{car.Category?.CategoryName ?? "Araç"} — {car.PlateNumber}"
                    : "Araç bilgisi";

                var discountCode = GenerateDiscountCode();

                try
                {
                    await _emailService.SendRentalApprovalEmailAsync(
                        customer.Email,
                        $"{customer.Name} {customer.Surname}",
                        carInfo,
                        rental.PickupDate,
                        rental.ReturnDate,
                        pickupBranch?.BranchName ?? "—",
                        returnBranch?.BranchName ?? "—",
                        rental.TotalPrice,
                        discountCode
                    );
                    TempData["Success"] = "Rezervasyon onaylandı ve müşteriye mail gönderildi.";
                }
                catch (Exception ex)
                {
                    TempData["Success"] = "Rezervasyon onaylandı ancak mail gönderilemedi: " + ex.Message;
                }
            }
            else
            {
                TempData["Success"] = "Rezervasyon onaylandı (müşterinin e-posta adresi bulunamadı).";
            }

            return RedirectToAction("RentalList");
        }

        [HttpPost]
        public async Task<IActionResult> RejectRental(int id)
        {
            var rental = await _rentalService.TGetByIdAsync(id);
            if (rental == null)
            {
                TempData["Error"] = "Rezervasyon bulunamadı.";
                return RedirectToAction("RentalList");
            }

            rental.Status = "Reddedildi";
            await _rentalService.TUpdateAsync(rental);
            TempData["Success"] = "Rezervasyon reddedildi.";
            return RedirectToAction("RentalList");
        }

        public async Task<IActionResult> DeleteRental(int id)
        {
            await _rentalService.TDeleteAsync(id);
            TempData["Success"] = "Rezervasyon kaydı silindi.";
            return RedirectToAction("RentalList");
        }

        private string GenerateDiscountCode()
        {
            var random = new Random();
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var code = new char[8];
            for (int i = 0; i < 8; i++)
                code[i] = chars[random.Next(chars.Length)];
            return "RENT-" + new string(code, 0, 4) + "-" + new string(code, 4, 4);
        }
    }
}
