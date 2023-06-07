using App.Contracts.BLL;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Mappers;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Api controller for Topics
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TopicController : ControllerBase
{
  private readonly IAppBll _bll;
  private readonly TopicMapper _mapper;

  /// <summary>
  /// Topic constructor
  /// </summary>
  /// <param name="bll"></param>
  /// <param name="mapper"></param>
  public TopicController(IAppBll bll, IMapper mapper)
  {
    _bll = bll;
    _mapper = new TopicMapper(mapper);
  }

  /// <summary>
  /// Return topics
  /// </summary>
  /// <returns></returns>
  [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Topic>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
  {
    var items = await _bll.Topics.GetAllAsync(User.GetUserId());
    return Ok(items.Select(i => _mapper.Map(i)));
  }

  // GET: api/Subjects/5
  /// <summary>
  /// Get one topic by id(not used)
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(Topic), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<Topic>> GetTopic(Guid id)
  {
    var item = await _bll.Topics.FirstOrDefaultAsync(id, User.GetUserId());
    if (item == null)
      return NotFound();

    var mapped = _mapper.Map(item);
    if (mapped == null)
      return NotFound();

    return mapped;
  }

  // PUT: api/Subjects/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  /// <summary>
  /// Update topic by id
  /// </summary>
  /// <param name="id"></param>
  /// <param name="entity"></param>
  /// <returns></returns>
  [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> PutTopic(Guid id, Topic item)
  {
    if (!await _bll.Topics.ExistsAsync(id, User.GetUserId()))
      return NotFound();

    var bllItem = _mapper.Map(item);
    _bll.Topics.Update(bllItem, User.GetUserId());
    await _bll.SaveChangesAsync();

    return NoContent();
  }

  //
  // // POST: api/Subjects
  // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  /// <summary>
  /// Create topic
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  public async Task<ActionResult<Topic>> PostTopic(Topic item)
  {
    var bllItem = _mapper.Map(item);
    var addedItem = _bll.Topics.Add(bllItem);
    await _bll.SaveChangesAsync();

    var returnItem = _mapper.Map(addedItem);
    return CreatedAtAction(nameof(GetTopic), new { id = returnItem!.Id }, item);
  }

  //
  // // DELETE: api/Subjects/5
  /// <summary>
  /// Delete topic by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> DeleteTopic(Guid id)
  {
    if (!await _bll.Follows.ExistsAsync(id, User.GetUserId()))
      return NotFound();

    await _bll.Follows.RemoveAsync(id, User.GetUserId());
    await _bll.SaveChangesAsync();

    return NoContent();
  }
}