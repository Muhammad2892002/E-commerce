using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Services;
using myshop.DAL.Data;
using myshop.Domain.Models;
using Stripe;
using System.Diagnostics;

namespace myshop.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ProductServices _productService;

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,ProductServices productService,IMapper mapper)

        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
            
        }
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Index()
        {
            return View();
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
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DashBoard() {

            return View();
        
        
        }


        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> CustomerHome() {
            var allProductsDto = await _productService.GetAllProducts();
           var allProducts = _mapper.Map<List<ProductVM>>(allProductsDto);

            return View(allProducts);
        
        }
    }
}