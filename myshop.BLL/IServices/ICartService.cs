using Microsoft.AspNetCore.Http;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.IServices
{
    public interface ICartService
    {
        public Task AddProductToCartAsync(int id,ISession session);
        public Task<List<Cart>> GetAllProductFromCart(ISession session);
    }
}
