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
    public class UserHashtagsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly UserHashtagMapper _mapper;

        public UserHashtagsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UserHashtagMapper(mapper);
        }

        // GET: api/UserHashtag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHashtag>>> GetAll()
        {
            var items = await _bll.UserHashtags.GetAllAsync(User.GetUserId());
            return Ok(items.Select(i => _mapper.Map(i)));
        }

        // GET: api/UserHashtag/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserHashtag>> Get(Guid id)
        {
            var item = await _bll.UserHashtags.FirstOrDefaultAsync(id, User.GetUserId());
            if (item == null)
                return NotFound();

            var mapped = _mapper.Map(item);
            if (mapped == null)
                return NotFound();

            return mapped;
        }

        // POST: api/UserHashtag
        [HttpPost]
        public async Task<ActionResult<UserHashtag>> Post(UserHashtag item)
        {
            var bllItem = _mapper.Map(item);
            var addedItem = _bll.UserHashtags.Add(bllItem);
            await _bll.SaveChangesAsync();

            var returnItem = _mapper.Map(addedItem);
            return CreatedAtAction(nameof(Get), new {id = returnItem!.Id}, item);
        }

        // PUT: api/UserHashtag/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UserHashtag item)
        {
            if (!await _bll.UserHashtags.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            var bllItem = _mapper.Map(item);
            _bll.UserHashtags.Update(bllItem, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserHashtag/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _bll.UserHashtags.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            await _bll.UserHashtags.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}