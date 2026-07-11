using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface IUnitOfWork 
    {
         public ICategory Category { get; }
        public IProduct Product { get; }

        
        Task<int> SaveChangesAsync();
    }
}
