using System.Diagnostics;
using IdentityBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ChainOfResponsibility.ChainOfRes;
using WebApp.ChainOfResponsibility.Models;

namespace IdentityBase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IdentityContext _identityContext;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IdentityContext identityContext, IConfiguration configuration)
        {
            _logger = logger;
            _identityContext = identityContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> SendEmail(string fileName, string toEmail)
        {
            var products = await _identityContext.Products.ToListAsync();
            var excelProcessHandler = new ExcelProcessHandler<Product>();
            var zipProcessHandler = new ZipFileProcessHandler<Product>();
            var sendEmailProcessHandler = new SendEmailProcessHandler("product.zip", _configuration, "ayavuzisik@gmail.com");

            if (products.Any())
            {
                excelProcessHandler.SetNext(zipProcessHandler).SetNext(sendEmailProcessHandler);
                excelProcessHandler.Handle(products);
                _logger.LogCritical("E-posta baþarýyla gönderildi.");
            }
            else
            {
                _logger.LogWarning("Ürün listesi boþ, e-posta iþlemi iptal edildi.");
            }


            return View(nameof(Index));
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
