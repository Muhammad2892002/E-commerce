using AutoMapper;
using myshop.BLL.Dto;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    
    public class ProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddNewProduct(ProductDto obj)
        {
            try { 
                var product=_mapper.Map<Product>(obj);
                product.Category = null;
              var item =await _unitOfWork.Product.AddNewProduct(product);
                if (item)
                {
                    var result = await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                return false;
                

               
            
            
            }
            catch {
            
              return false;
            }
        
        }

        public async Task<List<ProductDto>> GetAllProducts() { 
        
             var allProducts=await _unitOfWork.Product.GetAllProducts();
            var allProductsAsDto=(from product in allProducts
                                  select new ProductDto() {
                                      Id = product.Id,
                                      Name = product.Name,
                                      Description = product.Description,
                                      CategoryName = product.Category.Name,
                                      CategoryId = product.Category.Id,
                                      Price = product.Price,
                                      Img = product.Img,


                                  }
                                  ).ToList();
            
            return allProductsAsDto;

        }

        public async Task<bool> EditProduct(ProductDto obj) {
            try
            {

                var product = _mapper.Map<Product>(obj);
                product.Category = null;
                var isUpdated= await _unitOfWork.Product.EditProduct(product);
                if (isUpdated)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else {
                    return false;

                }
                
            }
            catch { return false; }



        }


        public async Task<ProductDto?> GetProductById(int? id) { 
            var productAsEntity = await _unitOfWork.Product.GetProductById(id);
            var productAsDto = _mapper.Map<ProductDto>(productAsEntity);
            
            return productAsDto;

        }

        public async Task<bool> DeleteProduct(int? id) {
            var isDeleted = await _unitOfWork.Product.DeleteProduct(id);
            await _unitOfWork.SaveChangesAsync();
            return isDeleted;


        }

        public async Task<bool> checkProductIfExist(Product obj) {
            try {


                return true;
            
            }
            catch {


                return false;
            
            }
        
          
        
        
        }
    }
}
