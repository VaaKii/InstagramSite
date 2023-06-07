using App.Contracts.BLL;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class DirectMessagesController : Controller
    {
        private readonly IAppBll _bll;

        public DirectMessagesController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: DirectMessages
        public async Task<IActionResult> Index()
        {
            var items = _bll.DirectMessages.GetAll(User.GetUserId());
            return View(items);
        }

        // GET: DirectMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _bll.DirectMessages == null)
            {
                return NotFound();
            }

            var directMessage = await _bll.DirectMessages
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers, "Id", "Firstname");
            return View();
        }

        // POST: DirectMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] DirectMessage directMessage)
        {
            if (ModelState.IsValid)
            {
                directMessage.Id = Guid.NewGuid();
                _bll.Add(directMessage);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers, "Id", "Firstname", directMessage.AppUserId);
            return View(directMessage);
        }

        // GET: DirectMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _bll.DirectMessages == null)
            {
                return NotFound();
            }

            var directMessage = await _bll.DirectMessages.FindAsync(id);
            if (directMessage == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers, "Id", "Firstname", directMessage.AppUserId);
            return View(directMessage);
        }

        // POST: DirectMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] DirectMessage directMessage)
        {
            if (id != directMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Update(directMessage);
                    await _bll.SaveChangesAsync();
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers, "Id", "Firstname", directMessage.AppUserId);
            return View(directMessage);
        }

        // GET: DirectMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _bll.DirectMessages == null)
            {
                return NotFound();
            }

            var directMessage = await _bll.DirectMessages
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
            if (_bll.DirectMessages == null)
            {
                return Problem("Entity set 'IAppBll.DirectMessages'  is null.");
            }
            var directMessage = await _bll.DirectMessages.FindAsync(id);
            if (directMessage != null)
            {
                _bll.DirectMessages.Remove(directMessage);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectMessageExists(Guid id)
        {
          return (_bll.DirectMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
