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
            Console.WriteLine("Testing");
            
                //return RedirectToAction("Success");
            var User = from m in _dataContext.Users select m;
            User = User.Where(s => s.UserName.Contains(user.UserName));
            if (User.Count() != 0)
            {
                if (User.First().PassWord == user.PassWord)
                {
                    return RedirectToAction("Success");
                }
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

