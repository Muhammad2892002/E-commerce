using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Services;
using myshop.Domain.Models;
using System.Runtime.CompilerServices;

namespace myshop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ProductServices _productService;
        private readonly IMapper _mapper;

        public CustomerController(ProductServices productServices, IMapper mapper) { 
           
            _productService = productServices;
            _mapper = mapper;
        
        
        }

        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> CustomerHome()
        {

            var allProductsDto = await _productService.GetAllProducts();
            var allProducts = _mapper.Map<List<ProductVM>>(allProductsDto);

            return View(allProducts);

        }

      
    }
}
