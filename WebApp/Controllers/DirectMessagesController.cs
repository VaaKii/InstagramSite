using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class DirectMessagesController : Controller
    {
        private readonly AppDbContext _context;

        public DirectMessagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DirectMessages
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DirectMessages.Include(d => d.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DirectMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DirectMessages == null)
            {
                return NotFound();
            }

            var directMessage = await _context.DirectMessages
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directMessage == null)
            {
                return NotFound();
            }

            return View(directMessage);
        }

        // GET: DirectMessages/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname");
            return View();
        }

        // POST: DirectMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,Message,SenderId,CreatedAt,Id")] DirectMessage directMessage)
        {
            if (ModelState.IsValid)
            {
                directMessage.Id = Guid.NewGuid();
                _context.Add(directMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", directMessage.AppUserId);
            return View(directMessage);
        }

        // GET: DirectMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DirectMessages == null)
            {
                return NotFound();
            }

            var directMessage = await _context.DirectMessages.FindAsync(id);
            if (directMessage == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", directMessage.AppUserId);
            return View(directMessage);
        }

        // POST: DirectMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,Message,SenderId,CreatedAt,Id")] DirectMessage directMessage)
        {
            if (id != directMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectMessageExists(directMessage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", directMessage.AppUserId);
            return View(directMessage);
        }

        // GET: DirectMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DirectMessages == null)
            {
                return NotFound();
            }

            var directMessage = await _context.DirectMessages
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directMessage == null)
            {
                return NotFound();
            }

            return View(directMessage);
        }

        // POST: DirectMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DirectMessages == null)
            {
                return Problem("Entity set 'AppDbContext.DirectMessages'  is null.");
            }
            var directMessage = await _context.DirectMessages.FindAsync(id);
            if (directMessage != null)
            {
                _context.DirectMessages.Remove(directMessage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectMessageExists(Guid id)
        {
          return (_context.DirectMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
