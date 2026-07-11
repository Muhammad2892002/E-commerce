using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class CategoryRepo : ICategory
    {
        private readonly ApplicationDbContext _context;
       

        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public Task AddNewCategory(Domain.Models.Category obj)
        {
            try
            {
                
                _context.Categories.Add(obj);
                _context.SaveChanges();
                return Task.CompletedTask;

            }
            catch {
         
                throw ;
            
            }
        }

        public async Task<List<Domain.Models.Category>> AllCategoryAsync()
        {
           List<Category> allCategory= await _context.Categories.ToListAsync<Category>();
            return allCategory;
        }

        public async Task<bool> CheckCategoryExistince(Category obj)
        {
            if (obj.Id == 0)
            {
                var NormalizedName = obj.Name.ToUpper().Trim();
                var checkCategory = _context.Categories.Any(x => x.Name.ToUpper().Trim().Contains(NormalizedName));
                if (checkCategory) {
                    return false;
                 
                
                }
               
            }
            else if(obj.Id>0)
            {
                var NormalizedName = obj.Name.ToUpper().Trim();
                var checkCategory = _context.Categories.Any(x => x.Id!=obj.Id&&x.Name.ToUpper().Trim().Contains(NormalizedName));
                if (checkCategory) {
                    return false;
                
                }
               



            }
            return true;
               
           
           
        }

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges() ;

            }
            else
            {

                throw new Exception("Category do not exist");

            }
        }

    

       public async Task EditCategory(Domain.Models.Category obj)
        {
          _context.Categories.Update(obj);
            _context.SaveChangesAsync();
           
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
