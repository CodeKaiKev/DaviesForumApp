using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DaviesForumApp.Data;

using Microsoft.AspNetCore.Identity;
using DaviesForumApp.Models;

namespace DaviesForumApp.Controllers
{
    public class UserLogin : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _dataContext;

        public UserLogin(UserManager<User> userManager, SignInManager<User> signInManager, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(User user)
        //{
         //   if (!ModelState.IsValid) return View(user);

         //   var userR = await _userManager.FindByNameAsync(user.UserName);
//
         //   if(userR != null)
         //   {
        //        var passwordCheck = await _userManager.CheckPasswordAsync(userR, user.PassWord);
         ///       if(passwordCheck)
           //     {
           //         var result = await _signInManager.PasswordSignInAsync(userR, user.PassWord, false, false);
            //        if (result.Succeeded)
         //           {
         //               return RedirectToAction("Index", "MessagePost");
         //           }
         //       }
        //        TempData["Error"] = "Wrong Credentials. Please, try again";

        //    }
        }
    }

