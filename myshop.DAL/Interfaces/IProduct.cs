using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface IProduct
    {
        public Task<bool> AddNewProduct(Product obj);

        public Task<List<Product>> GetAllProducts();

        public Task<bool> EditProduct(Product obj);

        public Task<Product> GetProductById(int? Id);

        public Task<bool> DeleteProduct(int?Id);
    }
}
