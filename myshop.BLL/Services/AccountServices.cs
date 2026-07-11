using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class AccountServices
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountRepo;
       
      
        public AccountServices(IMapper mapper, IAccountRepo accountRepo,SignInManager<ApplicationUser> signInManager)
        {

            _mapper = mapper;
            _accountRepo = accountRepo;
         
        }

        public async Task<bool> SignUp(UserDto obj) {
            try
            {
                var UserAsEntity = _mapper.Map<ApplicationUser>(obj);
                var result = await _accountRepo.RegisterAsync(UserAsEntity, obj.Password);
                return result;
            }
            catch (Exception ex) { 
               
                throw new Exception($"An error occurred while signing up: {ex.Message}", ex);

            }
          
           
        
        
        }

        public async Task<string> Login(string email,string password,bool remeberMe) { 
            var result= await _accountRepo.Login(email, password,remeberMe);
            return result;



        }


        public async Task<bool> LogOut() { 


            var isSignedOut = await _accountRepo.LogOut();
            if (isSignedOut)
            {


                return true;
            }


            return false;

        }



    }
}
