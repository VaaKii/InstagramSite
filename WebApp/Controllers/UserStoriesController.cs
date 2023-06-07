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
    public class UserStoriesController : Controller
    {
        private readonly IAppBll _bll;
        private readonly UserStoryMapper _mapper;

        public UserStoriesController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserStoryMapper(mapper);
        }

        // GET: UserStories
        public async Task<IActionResult> Index()
        {
            var items = _bll.UserStories.GetAll(User.GetUserId());
            return View(_mapper.Map(items));
        }

        // GET: UserStories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _bll.UserStories == null)
            {
                return NotFound();
            }

            var UserStory = await _bll.UserStories.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserStory == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserStory));
        }

        // GET: UserStories/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname");
            return View();
        }

        // POST: UserStories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserStory UserStory)
        {
            if (ModelState.IsValid)
            {
                UserStory.Id = Guid.NewGuid();
                _bll.UserStories.Add(_mapper.Map(UserStory));
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserStory.AuthorId);
            return View(UserStory);
        }

        // GET: UserStories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _bll.UserStories == null)
            {
                return NotFound();
            }

            var UserStory = await _bll.UserStories.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserStory == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserStory.AuthorId);
            return View(_mapper.Map(UserStory));
        }

        // POST: UserStories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserStory UserStory)
        {
            if (id != UserStory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.UserStories.Update(_mapper.Map(UserStory));
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStoryExists(UserStory.Id))
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserStory.AuthorId);
            return View(UserStory);
        }

        // GET: UserStories/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _bll.UserStories == null)
            {
                return NotFound();
            }

            var UserStory = await _bll.UserStories
                .FirstOrDefaultAsync(id, User.GetUserId());
            if (UserStory == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserStory));
        }

        // POST: UserStories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_bll.UserStories == null)
            {
                return Problem("Entity set 'IAppBll.UserStories'  is null.");
            }
            var UserStory = await _bll.UserStories.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserStory != null)
            {
                _bll.UserStories.Remove(UserStory);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserStoryExists(Guid id)
        {
          return _bll.UserStories?.Exists(id, User.GetUserId()) ?? false;
        }
    }
}
