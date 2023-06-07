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
    public class UserPostsController : Controller
    {
        private readonly IAppBll _bll;
        private readonly UserPostMapper _mapper;

        public UserPostsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserPostMapper(mapper);
        }

        // GET: UserPosts
        public async Task<IActionResult> Index()
        {
            var items = _bll.UserPosts.GetAll(User.GetUserId());
            return View(_mapper.Map(items));
        }

        // GET: UserPosts/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _bll.UserPosts == null)
            {
                return NotFound();
            }

            var UserPost = await _bll.UserPosts.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserPost == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserPost));
        }

        // GET: UserPosts/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname");
            return View();
        }

        // POST: UserPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserPost UserPost)
        {
            if (ModelState.IsValid)
            {
                UserPost.Id = Guid.NewGuid();
                _bll.UserPosts.Add(_mapper.Map(UserPost));
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserPost.AuthorId);
            return View(UserPost);
        }

        // GET: UserPosts/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _bll.UserPosts == null)
            {
                return NotFound();
            }

            var UserPost = await _bll.UserPosts.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserPost == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserPost.AuthorId);
            return View(_mapper.Map(UserPost));
        }

        // POST: UserPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserPost UserPost)
        {
            if (id != UserPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.UserPosts.Update(_mapper.Map(UserPost));
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPostExists(UserPost.Id))
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserPost.AuthorId);
            return View(UserPost);
        }

        // GET: UserPosts/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _bll.UserPosts == null)
            {
                return NotFound();
            }

            var UserPost = await _bll.UserPosts
                .FirstOrDefaultAsync(id, User.GetUserId());
            if (UserPost == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserPost));
        }

        // POST: UserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_bll.UserPosts == null)
            {
                return Problem("Entity set 'IAppBll.UserPosts'  is null.");
            }
            var UserPost = await _bll.UserPosts.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserPost != null)
            {
                _bll.UserPosts.Remove(UserPost);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPostExists(Guid id)
        {
          return _bll.UserPosts?.Exists(id, User.GetUserId()) ?? false;
        }
    }
}
