using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userPosts = await _context.UserPosts.ToListAsync();
            var model = new UserPostViewModel
            {
                Posts = PaginatedList<UserPost>.Create(userPosts, 1, 10)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult<UserLike>> Post(Guid userId,Guid postId)
        {
            var post = await _context.UserPosts.FindAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            post.UserLikes?.Add(new UserLike{AppUserId = userId,UserPostId = postId});
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}