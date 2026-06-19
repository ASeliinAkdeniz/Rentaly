// ============================================
// OurFutureController.cs
// Areas/Admin/Controllers/OurFutureController.cs
// ============================================
using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurFutureController : Controller
    {
        private readonly IOurFutureService _ourFutureService;

        public OurFutureController(IOurFutureService ourFutureService)
        {
            _ourFutureService = ourFutureService;
        }

        public async Task<IActionResult> OurFutureList()
        {
            ViewData["Title"] = "Geleceğimiz";
            ViewData["ActiveMenu"] = "OurFuture";
            ViewData["BreadcrumbParent"] = "İçerik Yönetimi";
            var values = await _ourFutureService.TGetListAsync();
            return View("OurFutureList", values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOurFuture(OurFuture ourFuture)
        {
            ourFuture.ImageUrl = ourFuture.ImageUrl ?? "";
            await _ourFutureService.TInsertAsync(ourFuture);
            TempData["Success"] = "İçerik başarıyla eklendi.";
            return RedirectToAction("OurFutureList");
        }

        public async Task<IActionResult> DeleteOurFuture(int id)
        {
            await _ourFutureService.TDeleteAsync(id);
            TempData["Success"] = "İçerik silindi.";
            return RedirectToAction("OurFutureList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOurFuture(OurFuture ourFuture)
        {
            ourFuture.ImageUrl = ourFuture.ImageUrl ?? "";
            await _ourFutureService.TUpdateAsync(ourFuture);
            TempData["Success"] = "İçerik güncellendi.";
            return RedirectToAction("OurFutureList");
        }
    }
}


