using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
         public ICategory Category { get; }
        public IProduct Product { get; }
      
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepo(context);
            Product = new ProductRepo(context);
           
        }




        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
              
                Console.WriteLine(ex.ToString());
                throw;
            
            }
        }
    }
}
