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
    public class UserLikesController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserLikeMapper _mapper;

        public UserLikesController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserLikeMapper(mapper);
        }

        // GET: api/UserLike
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLike>>> GetAll()
        {
            var items = await _bll.UserLikes.GetAllAsync(User.GetUserId());
            return Ok(items.Select(i => _mapper.Map(i)));
        }

        // GET: api/UserLike/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserLike>> Get(Guid id)
        {
            var item = await _bll.UserLikes.FirstOrDefaultAsync(id, User.GetUserId());
            if (item == null)
                return NotFound();

            var mapped = _mapper.Map(item);
            if (mapped == null)
                return NotFound();

            return mapped;
        }

        // POST: api/UserLike
        [HttpPost]
        public async Task<ActionResult<UserLike>> Post(UserLike item)
        {
            var bllItem = _mapper.Map(item);
            var addedItem = _bll.UserLikes.Add(bllItem);
            await _bll.SaveChangesAsync();

            var returnItem = _mapper.Map(addedItem);
            return CreatedAtAction(nameof(Get), new {id = returnItem!.Id}, item);
        }

        // PUT: api/UserLike/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UserLike item)
        {
            if (!await _bll.UserLikes.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            var bllItem = _mapper.Map(item);
            _bll.UserLikes.Update(bllItem, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserLike/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _bll.UserLikes.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            await _bll.UserLikes.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}