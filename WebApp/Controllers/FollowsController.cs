using App.Contracts.BLL;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Mappers;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class FollowsController : Controller
    {
        private readonly IAppBll _bll;
        private readonly FollowMapper _mapper;

        public FollowsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new FollowMapper(mapper);
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var items = _bll.Follows.GetAll(User.GetUserId());
            return View(_mapper.Map(items));
        }

        // GET: Follows/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _bll.Follows == null)
            {
                return NotFound();
            }

            var Follow = await _bll.Follows.FirstOrDefaultAsync(id, User.GetUserId());
            if (Follow == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(Follow));
        }

        // GET: Follows/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname");
            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] Follow Follow)
        {
            if (ModelState.IsValid)
            {
                Follow.Id = Guid.NewGuid();
                _bll.Follows.Add(_mapper.Map(Follow));
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", Follow.AuthorId);
            return View(Follow);
        }

        // GET: Follows/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _bll.Follows == null)
            {
                return NotFound();
            }

            var Follow = await _bll.Follows.FirstOrDefaultAsync(id, User.GetUserId());
            if (Follow == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", Follow.AuthorId);
            return View(_mapper.Map(Follow));
        }

        // POST: Follows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] Follow Follow)
        {
            if (id != Follow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Follows.Update(_mapper.Map(Follow));
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowExists(Follow.Id))
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", Follow.AuthorId);
            return View(Follow);
        }

        // GET: Follows/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _bll.Follows == null)
            {
                return NotFound();
            }

            var Follow = await _bll.Follows
                .FirstOrDefaultAsync(id, User.GetUserId());
            if (Follow == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(Follow));
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_bll.Follows == null)
            {
                return Problem("Entity set 'IAppBll.Follows'  is null.");
            }
            var Follow = await _bll.Follows.FirstOrDefaultAsync(id, User.GetUserId());
            if (Follow != null)
            {
                _bll.Follows.Remove(Follow);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(Guid id)
        {
          return _bll.Follows?.Exists(id, User.GetUserId()) ?? false;
        }
    }
}
