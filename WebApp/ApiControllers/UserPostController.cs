using App.Public.DTO.v1;
using App.Public.DTO.v1.Mappers;
using App.Contracts.BLL;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserPostsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserPostMapper _mapper;

        public UserPostsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserPostMapper(mapper);
        }

        // GET: api/Liked
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPost>>> GetAll()
        {
            var items = await _bll.UserPosts.GetAllAsync(User.GetUserId());
            return Ok(items.Select(i => _mapper.Map(i)));
        }

        // GET: api/Liked/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserPost>> Get(Guid id)
        {
            var item = await _bll.UserPosts.FirstOrDefaultAsync(id, User.GetUserId());
            if (item == null)
                return NotFound();

            var mapped = _mapper.Map(item);
            if (mapped == null)
                return NotFound();

            return mapped;
        }

        // POST: api/Liked
        [HttpPost]
        public async Task<ActionResult<UserPost>> Post(UserPost item)
        {
            var bllItem = _mapper.Map(item);
            var addedItem = _bll.UserPosts.Add(bllItem);
            await _bll.SaveChangesAsync();

            var returnItem = _mapper.Map(addedItem);
            return CreatedAtAction(nameof(Get), new {id = returnItem!.Id}, item);
        }

        // PUT: api/Liked/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UserPost item)
        {
            if (!await _bll.UserPosts.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            var bllItem = _mapper.Map(item);
            _bll.UserPosts.Update(bllItem, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Liked/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _bll.UserPosts.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            await _bll.UserPosts.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}