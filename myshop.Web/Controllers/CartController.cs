using Microsoft.AspNetCore.Mvc;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using Stripe;

namespace myshop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
      
        public CartController(ICartService cartService) { 
        _cartService = cartService;
        
        
        }
        public async Task<IActionResult> Index()
        {
            TempData["countOfProducts"] = HttpContext.Session.Keys.Count();

            var allProducts= await _cartService.GetAllProductFromCart( HttpContext.Session); 
                return View(allProducts);
        }
        [HttpPost]
        public async Task<IActionResult> AddCart(int productId) {
           
            await _cartService.AddProductToCartAsync(productId,HttpContext.Session);
            var count=HttpContext.Session.Keys.Count();
            TempData["countOfProducts"] = count;
        
           return RedirectToAction("CustomerHome","Customer");
        
        }
    }
}
