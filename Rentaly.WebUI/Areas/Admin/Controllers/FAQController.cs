using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQController : Controller
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IActionResult> FAQList()
        {
            ViewData["Title"] = "Sık Sorulan Sorular";
            ViewData["ActiveMenu"] = "FAQ";
            ViewData["BreadcrumbParent"] = "İçerik Yönetimi";
            var values = await _faqService.TGetListAsync();
            return View("FAQList", values.OrderBy(f => f.OrderIndex).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateFAQ(FAQ faq)
        {
            faq.Question = faq.Question ?? "";
            faq.Answer = faq.Answer ?? "";
            await _faqService.TInsertAsync(faq);
            TempData["Success"] = "Soru başarıyla eklendi.";
            return RedirectToAction("FAQList");
        }

        public async Task<IActionResult> DeleteFAQ(int id)
        {
            await _faqService.TDeleteAsync(id);
            TempData["Success"] = "Soru silindi.";
            return RedirectToAction("FAQList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFAQ(FAQ faq)
        {
            faq.Question = faq.Question ?? "";
            faq.Answer = faq.Answer ?? "";
            await _faqService.TUpdateAsync(faq);
            TempData["Success"] = "Soru güncellendi.";
            return RedirectToAction("FAQList");
        }
    }
}
