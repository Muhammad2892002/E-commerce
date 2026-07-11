using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using myshop.BLL.Dto;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;




namespace myshop.BLL.Services
{
    public class CategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryServices(IUnitOfWork unit,IMapper mapper) { 
         _mapper=mapper;
            _unitOfWork = unit;
        
        }
        public async Task<bool> AddCategory(CategoryDto? obj)
        {

            var category = _mapper.Map<Category>(obj);
            bool result = await _unitOfWork.Category.CheckCategoryExistince(category);
            if (result)
            {

                await _unitOfWork.Category.AddNewCategory(category);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<List<Category>> GetAllcategories() {
            var allCats= await _unitOfWork.Category.AllCategoryAsync();
       
            return allCats;
            
        
        
        }

        public async Task<CategoryDto> GetCategoryById(int id ) {



            var category= await _unitOfWork.Category.GetCategoryById(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

           
            return categoryDto;
        }

        public async Task<bool> EditCategory(CategoryDto obj) { 
           var catObj= _mapper.Map<Category>(obj);
            bool result = await _unitOfWork.Category.CheckCategoryExistince(catObj);
            if (result)
            {

                await _unitOfWork.Category.EditCategory(catObj);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else {
                return false;
                
              
                
            
            }
        
        }

        public async Task DeleteCategory(int id) {
          
            await _unitOfWork.Category.DeleteCategory(id);
            await _unitOfWork.SaveChangesAsync();
        
        }

        

        
    }
}
