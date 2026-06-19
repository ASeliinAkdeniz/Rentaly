using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IBranchService _branchService;
        private readonly IBrandService _brandService;
        private readonly ICarModelService _carModelService;

        public CarController(ICarService carService, ICategoryService categoryService,
            IBranchService branchService, IBrandService brandService, ICarModelService carModelService)
        {
            _carService = carService;
            _categoryService = categoryService;
            _branchService = branchService;
            _brandService = brandService;
            _carModelService = carModelService;
        }

        public async Task<IActionResult> CarList()
        {
            ViewData["Title"] = "Araç Listesi";
            ViewData["ActiveMenu"] = "Car";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            var values = await _carService.TGetAllCarsWithCategoryAsync();

            // Marka ve model isimlerini ViewBag ile gönder
            ViewBag.Brands = (await _brandService.TGetListAsync())
                .ToDictionary(b => b.BrandId, b => b.BrandName);
            ViewBag.CarModels = (await _carModelService.TGetListAsync())
                .ToDictionary(m => m.ModelId, m => m.ModelName);

            return View("CarList", values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            ViewData["Title"] = "Yeni Araç Ekle";
            ViewData["ActiveMenu"] = "CarCreate";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            await FillViewBags();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            await _carService.TInsertAsync(car);
            TempData["Success"] = "Araç başarıyla eklendi.";
            return RedirectToAction("CarList");
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.TDeleteAsync(id);
            TempData["Success"] = "Araç silindi.";
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            ViewData["Title"] = "Araç Düzenle";
            ViewData["ActiveMenu"] = "Car";
            ViewData["BreadcrumbParent"] = "Araç Yönetimi";
            await FillViewBags();
            var value = await _carService.TGetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(Car car)
        {
            await _carService.TUpdateAsync(car);
            TempData["Success"] = "Araç güncellendi.";
            return RedirectToAction("CarList");
        }

        [HttpGet]
        public async Task<IActionResult> GetCarJson(int id)
        {
            var value = await _carService.TGetByIdAsync(id);
            return Json(value);
        }

        private async Task FillViewBags()
        {
            ViewBag.Categories = new SelectList(await _categoryService.TGetListAsync(), "CategoryId", "CategoryName");
            ViewBag.Branches = new SelectList(await _branchService.TGetListAsync(), "BranchId", "BranchName");
            ViewBag.Brands = new SelectList(await _brandService.TGetListAsync(), "BrandId", "BrandName");
            ViewBag.Models = new SelectList(await _carModelService.TGetListAsync(), "ModelId", "ModelName");
        }
    }
}