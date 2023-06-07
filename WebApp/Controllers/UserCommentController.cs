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
    public class UserCommentController : Controller
    {
        private readonly IAppBll _bll;
        private readonly UserCommentMapper _mapper;

        public UserCommentController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserCommentMapper(mapper);
        }

        // GET: UserComments
        public async Task<IActionResult> Index()
        {
            var items = _bll.UserComments.GetAll(User.GetUserId());
            return View(_mapper.Map(items));
        }

        // GET: UserComments/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _bll.UserComments == null)
            {
                return NotFound();
            }

            var UserComment = await _bll.UserComments.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserComment == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserComment));
        }

        // GET: UserComments/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname");
            return View();
        }

        // POST: UserComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserComment UserComment)
        {
            if (ModelState.IsValid)
            {
                UserComment.Id = Guid.NewGuid();
                _bll.UserComments.Add(_mapper.Map(UserComment));
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserComment.AuthorId);
            return View(UserComment);
        }

        // GET: UserComments/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _bll.UserComments == null)
            {
                return NotFound();
            }

            var UserComment = await _bll.UserComments.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserComment == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserComment.AuthorId);
            return View(_mapper.Map(UserComment));
        }

        // POST: UserComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] UserComment UserComment)
        {
            if (id != UserComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.UserComments.Update(_mapper.Map(UserComment));
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCommentExists(UserComment.Id))
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", UserComment.AuthorId);
            return View(UserComment);
        }

        // GET: UserComments/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _bll.UserComments == null)
            {
                return NotFound();
            }

            var UserComment = await _bll.UserComments
                .FirstOrDefaultAsync(id, User.GetUserId());
            if (UserComment == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(UserComment));
        }

        // POST: UserComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_bll.UserComments == null)
            {
                return Problem("Entity set 'IAppBll.UserComments'  is null.");
            }
            var UserComment = await _bll.UserComments.FirstOrDefaultAsync(id, User.GetUserId());
            if (UserComment != null)
            {
                _bll.UserComments.Remove(UserComment);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCommentExists(Guid id)
        {
          return _bll.UserComments?.Exists(id, User.GetUserId()) ?? false;
        }
    }
}
