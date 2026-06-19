using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AwardController : Controller
    {
        private readonly IAwardService _awardService;

        public AwardController(IAwardService awardService)
        {
            _awardService = awardService;
        }

        public async Task<IActionResult> AwardList()
        {
            ViewData["Title"] = "Ödüller";
            ViewData["ActiveMenu"] = "Award";
            ViewData["BreadcrumbParent"] = "İçerik Yönetimi";
            var values = await _awardService.TGetListAsync();
            return View("AwardList", values.OrderByDescending(a => a.Year).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAward(Award award)
        {
            award.ImageUrl = award.ImageUrl ?? "";
            award.Description = award.Description ?? "";
            await _awardService.TInsertAsync(award);
            TempData["Success"] = "Ödül başarıyla eklendi.";
            return RedirectToAction("AwardList");
        }

        public async Task<IActionResult> DeleteAward(int id)
        {
            await _awardService.TDeleteAsync(id);
            TempData["Success"] = "Ödül silindi.";
            return RedirectToAction("AwardList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAward(Award award)
        {
            award.ImageUrl = award.ImageUrl ?? "";
            award.Description = award.Description ?? "";
            await _awardService.TUpdateAsync(award);
            TempData["Success"] = "Ödül güncellendi.";
            return RedirectToAction("AwardList");
        }
    }
}
