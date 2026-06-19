using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task<IActionResult> StatisticList()
        {
            ViewData["Title"] = "İstatistikler";
            ViewData["ActiveMenu"] = "Statistic";
            ViewData["BreadcrumbParent"] = "İçerik Yönetimi";
            var values = await _statisticService.TGetListAsync();
            return View("StatisticList", values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatistic(Statistic statistic)
        {
            statistic.IconUrl = statistic.IconUrl ?? "";
            await _statisticService.TInsertAsync(statistic);
            TempData["Success"] = "İstatistik başarıyla eklendi.";
            return RedirectToAction("StatisticList");
        }

        public async Task<IActionResult> DeleteStatistic(int id)
        {
            await _statisticService.TDeleteAsync(id);
            TempData["Success"] = "İstatistik silindi.";
            return RedirectToAction("StatisticList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatistic(Statistic statistic)
        {
            statistic.IconUrl = statistic.IconUrl ?? "";
            await _statisticService.TUpdateAsync(statistic);
            TempData["Success"] = "İstatistik güncellendi.";
            return RedirectToAction("StatisticList");
        }
    }
}
