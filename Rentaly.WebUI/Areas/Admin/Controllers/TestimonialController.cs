using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> TestimonialList()
        {
            ViewData["Title"] = "Testimonial";
            ViewData["ActiveMenu"] = "Testimonial";
            ViewData["BreadcrumbParent"] = "İçerik Yönetimi";
            var values = await _testimonialService.TGetListAsync();
            return View("TestimonialList", values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(Testimonial testimonial)
        {
            testimonial.ImageUrl = testimonial.ImageUrl ?? "";
            testimonial.Comment = testimonial.Comment ?? "";
            await _testimonialService.TInsertAsync(testimonial);
            TempData["Success"] = "Yorum başarıyla eklendi.";
            return RedirectToAction("TestimonialList");
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _testimonialService.TDeleteAsync(id);
            TempData["Success"] = "Yorum silindi.";
            return RedirectToAction("TestimonialList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(Testimonial testimonial)
        {
            testimonial.ImageUrl = testimonial.ImageUrl ?? "";
            testimonial.Comment = testimonial.Comment ?? "";
            await _testimonialService.TUpdateAsync(testimonial);
            TempData["Success"] = "Yorum güncellendi.";
            return RedirectToAction("TestimonialList");
        }
    }
}