using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public async Task<IActionResult> BranchList()
        {
            ViewData["Title"] = "Şubeler";
            ViewData["ActiveMenu"] = "Branch";
            ViewData["BreadcrumbParent"] = "Operasyon";
            var values = await _branchService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBranch()
        {
            ViewData["Title"] = "Yeni Şube";
            ViewData["ActiveMenu"] = "Branch";
            ViewData["BreadcrumbParent"] = "Operasyon";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(Branch branch)
        {
            await _branchService.TInsertAsync(branch);
            TempData["Success"] = "Şube başarıyla eklendi.";
            return RedirectToAction("BranchList");
        }

        public async Task<IActionResult> DeleteBranch(int id)
        {
            await _branchService.TDeleteAsync(id);
            TempData["Success"] = "Şube silindi.";
            return RedirectToAction("BranchList");
        }

        [HttpGet]
        public async Task<IActionResult> GetBranchJson(int id)
        {
            var value = await _branchService.TGetByIdAsync(id);
            return Json(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBranch(Branch branch)
        {
            await _branchService.TUpdateAsync(branch);
            TempData["Success"] = "Şube güncellendi.";
            return RedirectToAction("BranchList");
        }
    }
}