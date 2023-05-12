using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DaviesForumApp.Data;
using DaviesForumApp.Models;
using Microsoft.PowerBI.Api.Models;
using Microsoft.PowerBI.Api;

namespace DaviesForumApp.Controllers
{
   
    public class MessagePostsController : Controller
    {
        private readonly DataContext _context;



        public MessagePostsController(DataContext context)
        {
            _context = context;
        }
        //Search Bar
        public async Task<IActionResult> Search(string searching)
        {
            var dataContext = _context.Posts.Include(m => m.User);
            return View(dataContext.Where(x => x.Title.StartsWith(searching) || searching == null).ToList());
        }
        
        public async Task<IActionResult> Searcher(string searching)
        {
            return View(_context.Posts.Where(x => x.Title.StartsWith(searching) || searching == null).ToList());
        }
        // GET: MessagePosts
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Posts.Include(m => m.User);
            return View(await dataContext.ToListAsync());
        }

        // GET: MessagePosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var messagePost = await _context.Posts
                .Include(m => m.User)
                .Include(k => k.Replies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messagePost == null)
            {
                return NotFound();
            }

            return View(messagePost);
        }

        // GET: MessagePosts/Create
        public IActionResult Create()
            
        {
            //string check = HttpContext.Session.GetString("UserName");
            //var sessionUser = (from c in _context.Users
            //                      where c.UserName.Equals(check)
            //                      select c).SingleOrDefault();

            //ViewBag.UserId = sessionUser.UserId;
            //var sessionUser = (from c in _context.Users where c.UserName.Equals(HttpContext.Session.GetString("UserName")) select c).SingleOrDefault();
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName");
            return View();
        }

        // POST: MessagePosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MessagePost messagePost)
        {
           
            
            if (ModelState.IsValid)
            {
                //messagePost.UserId = (int)HttpContext.Session.GetInt32("UserId");
                _context.Add(messagePost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
            }
         
            return View(messagePost);
        }

        // GET: MessagePosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var messagePost = await _context.Posts.FindAsync(id);
            if (messagePost == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", messagePost.UserId);
            return View(messagePost);
        }

        // POST: MessagePosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MessagePost messagePost)
        {
            if (id != messagePost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messagePost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagePostExists(messagePost.Id))
                    {
                        System.Console.WriteLine("Error");
                        return NotFound();
                        
                    }
                    else
                    {
                        System.Console.WriteLine("Error");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Search));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", messagePost.UserId);
            return View(messagePost);
        }

        // GET: MessagePosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var messagePost = await _context.Posts
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messagePost == null)
            {
                return NotFound();
            }

            return View(messagePost);
        }

        // POST: MessagePosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'DataContext.Posts'  is null.");
            }
            var messagePost = await _context.Posts.FindAsync(id);
            if (messagePost != null)
            {
                _context.Posts.Remove(messagePost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Search));
        }

        private bool MessagePostExists(int id)
        {
          return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
