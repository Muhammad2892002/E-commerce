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
            var result = await _unitOfWork.Category.CreateAsync(category);
            if (result!=null)
            {

                await _unitOfWork.Category.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<List<Category>> GetAllcategories() {
            var allCats= await _unitOfWork.Category.GetAll();
       
            return allCats.ToList();
            
        
        
        }

        public async Task<CategoryDto> GetCategoryById(int id ) {



            var category= await _unitOfWork.Category.GetById(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

           
            return categoryDto;
        }

        public async Task<bool> EditCategory(CategoryDto obj) { 
           var catObj= _mapper.Map<Category>(obj);
            string result = await _unitOfWork.Category.UpdateAsync(catObj);
            if (result!=null)
            {

                await _unitOfWork.Category.UpdateAsync(catObj);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            else {
                return false;
                
              
                
            
            }
        
        }

        public async Task DeleteCategory(int id) {
          
            await _unitOfWork.Category.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        
        }

        

        
    }
}
