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
    public class UserStoryController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserStoryMapper _mapper;

        public UserStoryController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserStoryMapper(mapper);
        }

        // GET: api/UserStory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStory>>> GetAll()
        {
            var items = await _bll.UserStories.GetAllAsync(User.GetUserId());
            return Ok(items.Select(i => _mapper.Map(i)));
        }

        // GET: api/UserStory/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserStory>> Get(Guid id)
        {
            var item = await _bll.UserStories.FirstOrDefaultAsync(id, User.GetUserId());
            if (item == null)
                return NotFound();

            var mapped = _mapper.Map(item);
            if (mapped == null)
                return NotFound();

            return mapped;
        }

        // POST: api/UserStory
        [HttpPost]
        public async Task<ActionResult<UserStory>> Post(UserStory item)
        {
            var bllItem = _mapper.Map(item);
            var addedItem = _bll.UserStories.Add(bllItem);
            await _bll.SaveChangesAsync();

            var returnItem = _mapper.Map(addedItem);
            return CreatedAtAction(nameof(Get), new {id = returnItem!.Id}, item);
        }

        // PUT: api/UserStory/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UserStory item)
        {
            if (!await _bll.UserStories.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            var bllItem = _mapper.Map(item);
            _bll.UserStories.Update(bllItem, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserStory/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _bll.UserStories.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            await _bll.UserStories.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}