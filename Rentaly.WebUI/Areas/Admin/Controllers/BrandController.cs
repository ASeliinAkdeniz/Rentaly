using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> BrandList()
        {
            ViewData["Title"] = "Markalar";
            ViewData["ActiveMenu"] = "Brand";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            var values = await _brandService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            ViewData["Title"] = "Yeni Marka Ekle";
            ViewData["ActiveMenu"] = "Brand";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(Brand brand)
        {
            await _brandService.TInsertAsync(brand);
            return RedirectToAction("BrandList");
        }

        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandService.TDeleteAsync(id);
            return RedirectToAction("BrandList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            ViewData["Title"] = "Marka Düzenle";
            ViewData["ActiveMenu"] = "Brand";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            var value = await _brandService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBrand(Brand brand)
        {
            await _brandService.TUpdateAsync(brand);
            TempData["Success"] = "Marka başarıyla güncellendi.";
            return RedirectToAction("BrandList");
        }
    }
}