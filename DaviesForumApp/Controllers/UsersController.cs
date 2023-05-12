using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DaviesForumApp.Data;
using DaviesForumApp.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Scrypt;
using System.Text;

namespace DaviesForumApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }
        //Search Bar
        public async Task<IActionResult> Search(string searching)
        {
            
            return View(_context.Users.Where(x => x.UserName.StartsWith(searching) || searching == null).ToList());
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'DataContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
           
            var user = await _context.Users
                .Include(m => m.Posts)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( User user)
        {
            ScryptEncoder encoder= new ScryptEncoder();
            var registeredUser = (from c in _context.Users
                                 where c.UserName.Equals(user.UserName)
                                 select c).SingleOrDefault();
            if(registeredUser != null)
            {
                ViewBag.Error = "This username already registered.";
                return View();
            }

            
            if (ModelState.IsValid)
            {
                //Hashing Password
                Console.WriteLine(user.PassWord);
                user.PassWord = encoder.Encode(user.PassWord);
                Console.WriteLine(user.PassWord);
                user.ConfirmPassword = "";
                _context.Add(user);
                await _context.SaveChangesAsync();
                //var accountSid = "AC24d492a15cd6b997aa72021d05a0b452";
                //var authToken = "91080130215ecf62c914c218ffc0fa88";


                //TwilioClient.Init(accountSid, authToken);

                ////var mediaUrl = new[] { 
                ////    new Uri("https://upload.wikimedia.org/wikipedia/en/4/44/Fire_Emblem_Awakening_box_art.png")
                ////}.ToList();


                //var message = MessageResource.Create(
                //    body: "New user " + user.UserName + " has been created.",
                //    from: new Twilio.Types.PhoneNumber("+447700163581"),
                //    //mediaUrl: mediaUrl,
                //    to: new Twilio.Types.PhoneNumber("+447506402293")

                //);

                //Console.WriteLine(message.Sid);
                ViewBag.Error = "Registered successfully. Please login.";
                return RedirectToAction("Login", "UserLogin", new { area = "" });


            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  User user)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.PassWord = encoder.Encode(user.PassWord);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Search));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DataContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Search));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        
    }
}
