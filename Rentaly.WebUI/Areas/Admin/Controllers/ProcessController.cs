// ============================================
// ProcessController.cs
// Areas/Admin/Controllers/ProcessController.cs
// ============================================
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProcessController : Controller
    {
        private readonly IProcessService _processService;

        public ProcessController(IProcessService processService)
        {
            _processService = processService;
        }

        public async Task<IActionResult> ProcessList()
        {
            ViewData["Title"] = "İşleyiş";
            ViewData["ActiveMenu"] = "Process";
            ViewData["BreadcrumbParent"] = "İçerik Yönetimi";
            var values = await _processService.TGetListAsync();
            return View("ProcessList", values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProcess(Rentaly.EntityLayer.Entities.Process process)
        {
            process.IconUrl = process.IconUrl ?? "";
            await _processService.TInsertAsync(process);
            TempData["Success"] = "İşleyiş adımı başarıyla eklendi.";
            return RedirectToAction("ProcessList");
        }

        public async Task<IActionResult> DeleteProcess(int id)
        {
            await _processService.TDeleteAsync(id);
            TempData["Success"] = "İşleyiş adımı silindi.";
            return RedirectToAction("ProcessList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProcess(Rentaly.EntityLayer.Entities.Process process)
        {
            process.IconUrl = process.IconUrl ?? "";
            await _processService.TUpdateAsync(process);
            TempData["Success"] = "İşleyiş adımı güncellendi.";
            return RedirectToAction("ProcessList");
        }
    }
}