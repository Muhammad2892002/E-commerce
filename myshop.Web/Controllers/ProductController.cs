using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using myshop.BLL.Dto;
using myshop.BLL.Services;
using myshop.DAL.Data;
using myshop.Domain.Models;
using myshop.Web.ViewModels;


namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ProductController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;
        private readonly IMapper _mapper;
        public static List<ProductVM>? allProducts;

        public ProductController(IWebHostEnvironment webHostEnvironment, CategoryServices catSer, IMapper mapper, ProductServices productservice)
        {
            _mapper = mapper;

            _webHostEnvironment = webHostEnvironment;
            _categoryServices = catSer;
            _productServices = productservice;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var allProductsDto = await _productServices.GetAllProducts();
                allProducts = _mapper.Map<List<ProductVM>>(allProductsDto);


                return View(allProducts);

            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                // For simplicity, we'll just return the error message in the view.
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Redirect("www.google.com");
            }
        }

        //[HttpGet]
        //public IActionResult GetData()
        //{
        //    var products = _context.Products
        //        .Include(x => x.Category)
        //        .Select(x => new
        //        {
        //            id = x.Id,
        //            name = x.Name,
        //            description = x.Description,
        //            price = x.Price,
        //            categoryName = x.Category.Name
        //        })
        //        .ToList();

        //    return Json(new { data = products });
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                TempData["IsFailedToAdd"] = false;
                var allCats = (from cats in await _categoryServices.GetAllcategories()
                               select new CategoryVM()
                               {
                                   Id = cats.Id,
                                   Name = cats.Name,


                               }).ToList();
                ViewBag.AllCats = allCats;

                return View();
            }
            catch (Exception ex) {
                return View();

            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM, IFormFile? file)
        {
            try
            {
                //file = (IFormFile)productVM.Img;
                if (ModelState.IsValid)
                {
                    string RootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string filename = Guid.NewGuid().ToString();
                        var Upload = Path.Combine(RootPath, @"Images\Products");
                        var ext = Path.GetExtension(file.FileName);

                        using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                        {
                            file.CopyTo(filestream);
                        }
                        productVM.Img = @"Images\Products\" + filename + ext;
                    }
                    


                    var productObj = _mapper.Map<ProductDto>(productVM);
                    var result =await _productServices.AddNewProduct(productObj);
                    if (result) { 
                    TempData["Create"] = "Item has Created Successfully";
                        TempData["IsFailedToAdd"] = false;
                        return RedirectToAction("Index");
                    }
                    TempData["IsFailedToAdd"] = true;
                    var allCats = (from cats in await _categoryServices.GetAllcategories()
                                   select new CategoryVM()
                                   {
                                       Id = cats.Id,
                                       Name = cats.Name,


                                   }).ToList();
                    ViewBag.AllCats = allCats;
                }
                TempData["IsFailedToAdd"] = true;
                
                return View(productVM);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                // For simplicity, we'll just return the error message in the view.
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(productVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["IsFailedToAdd"] = false;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var allCats = (from cats in await _categoryServices.GetAllcategories()
                           select new CategoryVM()
                           {
                               Id = cats.Id,
                               Name = cats.Name,


                           }).ToList();
            ViewBag.AllCats = allCats;
            var productAsDto = await _productServices.GetProductById(id);
            var ProductAsVm = _mapper.Map<ProductVM>(productAsDto);

            var productExistince = ProductAsVm;
            if (productExistince != null)
            {
                return View(productExistince);

            }
            else {

                return RedirectToAction("Index");

            }


        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);

                    if (productVM.Img != null)
                    {
                        var oldimg = Path.Combine(RootPath, productVM.Img.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    productVM.Img = @"Images\Products\" + filename + ext;
                }

                var isUpdated = await _productServices.EditProduct(_mapper.Map<ProductDto>(productVM));
                if (!isUpdated) {
                    TempData["IsFailedToAdd"] = true;
                    var allCats = (from cats in await _categoryServices.GetAllcategories()
                                   select new CategoryVM()
                                   {
                                       Id = cats.Id,
                                       Name = cats.Name,


                                   }).ToList();
                    ViewBag.AllCats = allCats;

                    return View(productVM);
                
                }

                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? Id)
        {
            var IsDeleted =await _productServices.DeleteProduct(Id);


            return RedirectToAction("Index");
        }


    }
}
