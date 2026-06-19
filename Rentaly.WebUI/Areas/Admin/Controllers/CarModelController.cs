using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;

        public CarModelController(ICarModelService carModelService, IBrandService brandService)
        {
            _carModelService = carModelService;
            _brandService = brandService;
        }

        public async Task<IActionResult> CarModelList()
        {
            ViewData["Title"] = "Modeller";
            ViewData["ActiveMenu"] = "CarModel";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");
            var values = await _carModelService.TGetListAsync();
            return View("CarModelList", values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCarModel()
        {
            ViewData["Title"] = "Yeni Model Ekle";
            ViewData["ActiveMenu"] = "CarModel";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarModel(CarModel carModel)
        {
            await _carModelService.TInsertAsync(carModel);
            TempData["Success"] = "Model başarıyla eklendi.";
            return RedirectToAction("CarModelList");
        }

        public async Task<IActionResult> DeleteCarModel(int id)
        {
            await _carModelService.TDeleteAsync(id);
            TempData["Success"] = "Model silindi.";
            return RedirectToAction("CarModelList");
        }

        [HttpGet]
        public async Task<IActionResult> GetCarModelJson(int id)
        {
            var value = await _carModelService.TGetByIdAsync(id);
            return Json(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCarModel(CarModel carModel)
        {
            await _carModelService.TUpdateAsync(carModel);
            TempData["Success"] = "Model güncellendi.";
            return RedirectToAction("CarModelList");
        }
    }
}