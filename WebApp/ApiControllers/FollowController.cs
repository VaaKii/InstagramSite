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
    public class FollowsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly FollowMapper _mapper;

        public FollowsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new FollowMapper(mapper);
        }

        // GET: api/Follow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetAll()
        {
            var items = await _bll.Follows.GetAllAsync(User.GetUserId());
            return Ok(items.Select(i => _mapper.Map(i)));
        }

        // GET: api/Follow/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Follow>> Get(Guid id)
        {
            var item = await _bll.Follows.FirstOrDefaultAsync(id, User.GetUserId());
            if (item == null)
                return NotFound();

            var mapped = _mapper.Map(item);
            if (mapped == null)
                return NotFound();

            return mapped;
        }

        // POST: api/Follow
        [HttpPost]
        public async Task<ActionResult<Follow>> Post(Follow item)
        {
            var bllItem = _mapper.Map(item);
            var addedItem = _bll.Follows.Add(bllItem);
            await _bll.SaveChangesAsync();

            var returnItem = _mapper.Map(addedItem);
            return CreatedAtAction(nameof(Get), new {id = returnItem!.Id}, item);
        }

        // PUT: api/Follow/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, Follow item)
        {
            if (!await _bll.Follows.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            var bllItem = _mapper.Map(item);
            _bll.Follows.Update(bllItem, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Follow/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _bll.Follows.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            await _bll.Follows.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}