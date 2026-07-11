using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class AccounttRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccounttRepo(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser>signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        
        
        }

        public async Task<string> Login(string email, string password, bool rememberMe)
        {
            var userObj = await _userManager.FindByEmailAsync(email);
            if (userObj != null) { 
              
                var isPasswordValid = await _userManager.CheckPasswordAsync(userObj, password);
                if (isPasswordValid)
                {
                    if (userObj.LockoutEnd != null && userObj.LockoutEnd > DateTimeOffset.UtcNow) {
                        return "Your account is locked please contact with admin ";
                    }

                    await _signInManager.SignInAsync(userObj, isPersistent:rememberMe);
                   
                    return "LogedIn";


                }
                else {
                    
                    

                    return "Wrong Email or Password";
                
                }
               

            }
            return "The user it is not exist ";
        }

        public async Task<bool> LogOut()
        {
        await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<bool> RegisterAsync(ApplicationUser obj,string Password)
        {
            try
            {

                var isEmailExist = await _userManager.FindByEmailAsync(obj.Email);
                var isPhoneExist = await _userManager.Users.AnyAsync(u => u.PhoneNumber == obj.PhoneNumber);

                if (isEmailExist == null && !isPhoneExist)
                {
                    var result = await _userManager.CreateAsync(obj, Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(obj, "Customer");
                        return true;



                    }
                }
              
                    return false;
                
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            
            
            }
        }


        
    }
}
