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
    public class TopicsController : Controller
    {
        private readonly IAppBll _bll;
        private readonly TopicMapper _mapper;

        public TopicsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new TopicMapper(mapper);
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            var items = _bll.Topics.GetAll(User.GetUserId());
            return View(_mapper.Map(items));
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _bll.Topics == null)
            {
                return NotFound();
            }

            var Topic = await _bll.Topics.FirstOrDefaultAsync(id, User.GetUserId());
            if (Topic == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(Topic));
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname");
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] Topic Topic)
        {
            if (ModelState.IsValid)
            {
                Topic.Id = Guid.NewGuid();
                _bll.Topics.Add(_mapper.Map(Topic));
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", Topic.AuthorId);
            return View(Topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _bll.Topics == null)
            {
                return NotFound();
            }

            var Topic = await _bll.Topics.FirstOrDefaultAsync(id, User.GetUserId());
            if (Topic == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", Topic.AuthorId);
            return View(_mapper.Map(Topic));
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,Message,ReceiverId,CreatedAt,Id")] Topic Topic)
        {
            if (id != Topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Topics.Update(_mapper.Map(Topic));
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(Topic.Id))
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
            ViewData["AuthorId"] = new SelectList(_bll.AppUsers.GetAll(User.GetUserId()), "Id", "Firstname", Topic.AuthorId);
            return View(Topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _bll.Topics == null)
            {
                return NotFound();
            }

            var Topic = await _bll.Topics
                .FirstOrDefaultAsync(id, User.GetUserId());
            if (Topic == null)
            {
                return NotFound();
            }

            return View(_mapper.Map(Topic));
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_bll.Topics == null)
            {
                return Problem("Entity set 'IAppBll.Topics'  is null.");
            }
            var Topic = await _bll.Topics.FirstOrDefaultAsync(id, User.GetUserId());
            if (Topic != null)
            {
                _bll.Topics.Remove(Topic);
            }
            
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(Guid id)
        {
          return _bll.Topics?.Exists(id, User.GetUserId()) ?? false;
        }
    }
}
