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
    public class UserLikesController : Controller
    {
        private readonly IAppBll _bll;
        private readonly UserLikeMapper _mapper;

        public UserLikesController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserLikeMapper(mapper);
        }

        // GET: UserLikes
        public async Task<IActionResult> Index()
        {
            var items = _bll.UserLikes.GetAll(User.GetUserId());
            return View(_mapper.Map(items));
        }

        // GET: UserLikes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _bll.UserLikes == null)
            {
                return NotFound();
            }

            var UserLike = await _bll.UserLikes.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserLike == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserLike));
        }

        // GET: UserLikes/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname");
            return View();
        }

        // POST: UserLikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserLike UserLike)
        {
            if (ModelState.IsValid)
            {
                UserLike.Id = Guid.NewGuid();
                _bll.UserLikes.Add(_mapper.Map(UserLike));
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserLike.AuthorId);
            return View(UserLike);
        }

        // GET: UserLikes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _bll.UserLikes == null)
            {
                return NotFound();
            }

            var UserLike = await _bll.UserLikes.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserLike == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserLike.AuthorId);
            return View(_mapper.Map(UserLike));
        }

        // POST: UserLikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserLike UserLike)
        {
            if (id != UserLike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.UserLikes.Update(_mapper.Map(UserLike));
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLikeExists(UserLike.Id))
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserLike.AuthorId);
            return View(UserLike);
        }

        // GET: UserLikes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _bll.UserLikes == null)
            {
                return NotFound();
            }

            var UserLike = await _bll.UserLikes
                .FirstOrDefaultAsync(id, User.GetUserId());
            if (UserLike == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserLike));
        }

        // POST: UserLikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_bll.UserLikes == null)
            {
                return Problem("Entity set 'IAppBll.UserLikes'  is null.");
            }
            var UserLike = await _bll.UserLikes.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserLike != null)
            {
                _bll.UserLikes.Remove(UserLike);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLikeExists(Guid id)
        {
          return _bll.UserLikes?.Exists(id, User.GetUserId()) ?? false;
        }
    }
}
