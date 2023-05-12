using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DaviesForumApp.Data;
using DaviesForumApp.Models;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Scrypt;
using Microsoft.AspNetCore.Http;

namespace DaviesForumApp.Controllers
{
    public class UserLogin : Controller
    {
      
        private readonly DataContext _dataContext;

        public UserLogin(DataContext dataContext)
        {
           
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            Console.WriteLine("Testing");

            //return RedirectToAction("Success");
            var validUser = (from c in _dataContext.Users
                             where c.UserName.Equals(user.UserName)
                             select c).SingleOrDefault();
            if(validUser == null)
            {
                ViewBag.Error = "Username or Password invalid";
                return RedirectToAction("Fail");
            }
            bool isValidCustomer = encoder.Compare(user.PassWord, validUser.PassWord);
          
            if (isValidCustomer)
            {

                HttpContext.Session.SetString("UserName", user.UserName);



                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("Success");
               
            } 
            
            Console.WriteLine("Failed");
            return RedirectToAction("Fail");
        }

        public IActionResult Success()
        {
            
            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }
    }
}

