using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.WebUI.Models;

namespace Rentaly.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProcessService _processService;
        private readonly IOurFutureService _ourFutureService;
        private readonly IStatisticService _statisticService;
        private readonly IAwardService _awardService;
        private readonly ITestimonialService _testimonialService;
        private readonly IFAQService _faqService;
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly ICarModelService _carModelService;
        private readonly IBranchService _branchService;

        public HomeController(
            IProcessService processService,
            IOurFutureService ourFutureService,
            IStatisticService statisticService,
            IAwardService awardService,
            ITestimonialService testimonialService,
            IFAQService faqService,
            ICarService carService,
            IBrandService brandService,
            ICarModelService carModelService,
            IBranchService branchService)
        {
            _processService = processService;
            _ourFutureService = ourFutureService;
            _statisticService = statisticService;
            _awardService = awardService;
            _testimonialService = testimonialService;
            _faqService = faqService;
            _carService = carService;
            _brandService = brandService;
            _carModelService = carModelService;
            _branchService = branchService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeIndexViewModel
            {
                Processes = await _processService.TGetListAsync(),
                OurFutures = await _ourFutureService.TGetListAsync(),
                Statistics = await _statisticService.TGetListAsync(),
                Awards = await _awardService.TGetListAsync(),
                Testimonials = await _testimonialService.TGetListAsync(),
                FAQs = (await _faqService.TGetListAsync()).OrderBy(f => f.OrderIndex).ToList(),
                Brands = await _brandService.TGetListAsync(),
                Branches = await _branchService.TGetListAsync()
            };

            var allCars = await _carService.TGetAllCarsWithCategoryAsync();
            vm.FeaturedCars = allCars
                .Where(c => c.IsActive && c.IsAvailable)
                .Take(10)
                .ToList();

            return View(vm);
        }

        public async Task<IActionResult> About()
        {
            var vm = new AboutViewModel
            {
                Statistics = await _statisticService.TGetListAsync(),
                OurFutures = await _ourFutureService.TGetListAsync()
            };
            return View(vm);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NotFoundPage()
        {
            return View("NotFoundPage");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
