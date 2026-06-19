using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DTOLayer.CustomerDtos;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            ViewData["Title"] = "Müşteriler";
            ViewData["ActiveMenu"] = "Customer";
            ViewData["BreadcrumbParent"] = "Operasyon";
            var values = await _customerService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            ViewData["Title"] = "Yeni Müşteri";
            ViewData["ActiveMenu"] = "Customer";
            ViewData["BreadcrumbParent"] = "Operasyon";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            await _customerService.TInsertAsync(dto);
            TempData["Success"] = "Müşteri başarıyla eklendi.";
            return RedirectToAction("CustomerList");
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.TDeleteAsync(id);
            TempData["Success"] = "Müşteri silindi.";
            return RedirectToAction("CustomerList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            ViewData["Title"] = "Müşteri Düzenle";
            ViewData["ActiveMenu"] = "Customer";
            ViewData["BreadcrumbParent"] = "Operasyon";
            var value = await _customerService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto dto)
        {
            await _customerService.TUpdateAsync(dto);
            TempData["Success"] = "Müşteri güncellendi.";
            return RedirectToAction("CustomerList");
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerJson(int id)
        {
            var value = await _customerService.TGetByIdAsync(id);
            return Json(value);
        }
    }
}