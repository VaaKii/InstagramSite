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
    public class UserHashtagController : Controller
    {
        private readonly IAppBll _bll;
        private readonly UserHashtagMapper _mapper;

        public UserHashtagController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserHashtagMapper(mapper);
        }

        // GET: UserHashtags
        public async Task<IActionResult> Index()
        {
            var items = _bll.UserHashtags.GetAll(User.GetUserId());
            return View(_mapper.Map(items));
        }

        // GET: UserHashtags/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _bll.UserHashtags == null)
            {
                return NotFound();
            }

            var UserHashtag = await _bll.UserHashtags.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserHashtag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserHashtag));
        }

        // GET: UserHashtags/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname");
            return View();
        }

        // POST: UserHashtags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserHashtag UserHashtag)
        {
            if (ModelState.IsValid)
            {
                UserHashtag.Id = Guid.NewGuid();
                _bll.UserHashtags.Add(_mapper.Map(UserHashtag));
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserHashtag.AuthorId);
            return View(UserHashtag);
        }

        // GET: UserHashtags/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _bll.UserHashtags == null)
            {
                return NotFound();
            }

            var UserHashtag = await _bll.UserHashtags.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserHashtag == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserHashtag.AuthorId);
            return View(_mapper.Map(UserHashtag));
        }

        // POST: UserHashtags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserHashtag UserHashtag)
        {
            if (id != UserHashtag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.UserHashtags.Update(_mapper.Map(UserHashtag));
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserHashtagExists(UserHashtag.Id))
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserHashtag.AuthorId);
            return View(UserHashtag);
        }

        // GET: UserHashtags/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _bll.UserHashtags == null)
            {
                return NotFound();
            }

            var UserHashtag = await _bll.UserHashtags
                .FirstOrDefaultAsync(id, User.GetUserId());
            if (UserHashtag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserHashtag));
        }

        // POST: UserHashtags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_bll.UserHashtags == null)
            {
                return Problem("Entity set 'IAppBll.UserHashtags'  is null.");
            }
            var UserHashtag = await _bll.UserHashtags.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserHashtag != null)
            {
                _bll.UserHashtags.Remove(UserHashtag);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserHashtagExists(Guid id)
        {
          return _bll.UserHashtags?.Exists(id, User.GetUserId()) ?? false;
        }
    }
}
