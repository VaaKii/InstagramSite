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
    public class UserCommentsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserCommentMapper _mapper;

        public UserCommentsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserCommentMapper(mapper);
        }

        // GET: api/UserComment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserComment>>> GetAll()
        {
            var items = await _bll.UserComments.GetAllAsync(User.GetUserId());
            return Ok(items.Select(i => _mapper.Map(i)));
        }

        // GET: api/UserComment/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserComment>> Get(Guid id)
        {
            var item = await _bll.UserComments.FirstOrDefaultAsync(id, User.GetUserId());
            if (item == null)
                return NotFound();

            var mapped = _mapper.Map(item);
            if (mapped == null)
                return NotFound();

            return mapped;
        }

        // POST: api/UserComment
        [HttpPost]
        public async Task<ActionResult<UserComment>> Post(UserComment item)
        {
            var bllItem = _mapper.Map(item);
            var addedItem = _bll.UserComments.Add(bllItem);
            await _bll.SaveChangesAsync();

            var returnItem = _mapper.Map(addedItem);
            return CreatedAtAction(nameof(Get), new {id = returnItem!.Id}, item);
        }

        // PUT: api/UserComment/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UserComment item)
        {
            if (!await _bll.UserComments.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            var bllItem = _mapper.Map(item);
            _bll.UserComments.Update(bllItem, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserComment/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _bll.UserComments.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            await _bll.UserComments.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}