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
    public class DirectMessagesController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly DirectMessageMapper _mapper;

        public DirectMessagesController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new DirectMessageMapper(mapper);
        }

        // GET: api/DirectMessage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectMessage>>> GetAll()
        {
            var items = await _bll.DirectMessages.GetAllAsync(User.GetUserId());
            return Ok(items.Select(i => _mapper.Map(i)));
        }

        // GET: api/DirectMessage/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DirectMessage>> Get(Guid id)
        {
            var item = await _bll.DirectMessages.FirstOrDefaultAsync(id, User.GetUserId());
            if (item == null)
                return NotFound();

            var mapped = _mapper.Map(item);
            if (mapped == null)
                return NotFound();

            return mapped;
        }

        // POST: api/DirectMessage
        [HttpPost]
        public async Task<ActionResult<DirectMessage>> Post(DirectMessage item)
        {
            var bllItem = _mapper.Map(item);
            var addedItem = _bll.DirectMessages.Add(bllItem);
            await _bll.SaveChangesAsync();

            var returnItem = _mapper.Map(addedItem);
            return CreatedAtAction(nameof(Get), new {id = returnItem!.Id}, item);
        }

        // PUT: api/DirectMessage/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, DirectMessage item)
        {
            if (!await _bll.DirectMessages.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            var bllItem = _mapper.Map(item);
            _bll.DirectMessages.Update(bllItem, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/DirectMessage/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _bll.DirectMessages.ExistsAsync(id, User.GetUserId()))
                return NotFound();

            await _bll.DirectMessages.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}