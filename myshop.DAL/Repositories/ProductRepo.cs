using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class ProductRepo : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<bool> AddNewProduct(Product obj)
        {
            try {
                var isProductExist = await CheckIfProductExist(obj);
                if (isProductExist) {
                    return false;
                
                }

                await _context.Products.AddAsync(obj);
               
               
            
                return true;

            }
            catch {
                return false;


            }
        }

        public async Task<bool> DeleteProduct(int? Id)
        {
            try {

                var product = await GetProductById(Id);
                if (product != null)
                {
                    _context.Products.Remove(product);


                    return true;

                }
                else {

                    return false;

                }

            }
            catch {
                return false;


            }
        }

        public async Task<bool> EditProduct(Product obj)
        {
            try
            {
                var isProductExist = await CheckIfProductExist(obj);
                if (isProductExist) {

                    return false;
                
                }
                _context.Products.Update(obj);
               
                return true;
            }
            catch (Exception ex) {

                return false;

            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var allProducts = await _context.Products.Include(p => p.Category).ToListAsync();
            return allProducts;

        }

        public async Task<Product> GetProductById(int? Id)
        {
            var ProductVar = await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
            return ProductVar;
        }

        public async Task<bool> CheckIfProductExist(Product obj)
        {
            try {
                if (obj.Id == 0)
                {
                    var NormalizedProductname = obj.Name.Trim().ToUpper();
                    var isProductExist = await _context.Products.AnyAsync(p => p.Name.Trim().ToUpper().Contains(NormalizedProductname));
                    if (isProductExist)
                    {

                        return true;

                    }
                    
                }
                else {
                    var NormalizedProductname = obj.Name.Trim().ToUpper();
                    var isProductExist = await _context.Products.AnyAsync(p =>  p.Id!=obj.Id&& p.Name.Trim().ToUpper().Contains(NormalizedProductname));
                   return isProductExist;

                }
                return false;

               
            }
            catch {


                return false;
            }
        
        
        
        
        }
    }
}
