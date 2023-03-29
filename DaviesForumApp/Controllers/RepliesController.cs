using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DaviesForumApp.Data;
using DaviesForumApp.Models;

namespace DaviesForumApp.Controllers
{
    public class RepliesController : Controller
    {
        private readonly DataContext _context;

        public RepliesController(DataContext context)
        {
            _context = context;
        }
        //Search Bar
        public async Task<IActionResult> Search(string searching)
        {
            var dataContext = _context.Replies.Include(r => r.MessagePost);
            return View(dataContext.Where(x => x.Reply.StartsWith(searching) || searching == null).ToList());
        }
        // GET: Replies
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Replies.Include(r => r.MessagePost);
            return View(await dataContext.ToListAsync());
        }

        // GET: Replies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var replies = await _context.Replies
                .Include(r => r.MessagePost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (replies == null)
            {
                return NotFound();
            }

            return View(replies);
        }

        // GET: Replies/Create
        public IActionResult Create()
        {
            ViewData["MessagePostId"] = new SelectList(_context.Posts, "Id", "Message");
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reply,Name,MessagePostId")] Replies replies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(replies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
            }
            ViewData["MessagePostId"] = new SelectList(_context.Posts, "Id", "Message", replies.MessagePostId);
            return View(replies);
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var replies = await _context.Replies.FindAsync(id);
            if (replies == null)
            {
                return NotFound();
            }
            ViewData["MessagePostId"] = new SelectList(_context.Posts, "Id", "Message", replies.MessagePostId);
            return View(replies);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reply,Name,MessagePostId")] Replies replies)
        {
            if (id != replies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(replies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepliesExists(replies.Id))
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
            ViewData["MessagePostId"] = new SelectList(_context.Posts, "Id", "Message", replies.MessagePostId);
            return View(replies);
        }

        // GET: Replies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var replies = await _context.Replies
                .Include(r => r.MessagePost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (replies == null)
            {
                return NotFound();
            }

            return View(replies);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Replies == null)
            {
                return Problem("Entity set 'DataContext.Replies'  is null.");
            }
            var replies = await _context.Replies.FindAsync(id);
            if (replies != null)
            {
                _context.Replies.Remove(replies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Search));
        }

        private bool RepliesExists(int id)
        {
          return (_context.Replies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
