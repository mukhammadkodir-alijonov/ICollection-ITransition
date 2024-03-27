using DocumentFormat.OpenXml.Wordprocessing;
using ICollection.DataAccess.Interfaces.Common;
using ICollection.DataAccess.Repositories.Common;
using ICollection.Presentation.Models;
using ICollection.Service.Common.Utils;
using ICollection.Service.Interfaces.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ICollection.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ICollectionService collectionService)
        {
            _collectionService = collectionService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _collectionService.TopCollection(new PaginationParams(1, 10));
            return View(res);
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
