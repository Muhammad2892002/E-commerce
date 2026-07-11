using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface ICategory
    {
        public Task AddNewCategory(Category obj);
        public Task EditCategory(Category obj);

        public Task DeleteCategory(int id);

        public Task<List<Category>> 
            AllCategoryAsync();

        public Task<Category> GetCategoryById(int id);

        public Task<bool> CheckCategoryExistince(Category obj);


    }
}
