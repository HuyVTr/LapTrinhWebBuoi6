using System.Diagnostics;
using LapTrinhWebBuoi6.Models;
using LapTrinhWebBuoi6.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LapTrinhWebBuoi6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        // Trang chủ admin (giữ nguyên)
        public IActionResult Index()
        {
            return View();
        }

        // Trang shop cho member
        public async Task<IActionResult> Shop()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
