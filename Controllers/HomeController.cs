using Microsoft.AspNetCore.Mvc;
using mpolaris.Models;
using System.Diagnostics;

namespace mpolaris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
    }
}