using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mpolaris.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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