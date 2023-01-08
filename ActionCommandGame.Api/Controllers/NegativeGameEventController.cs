using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Results;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers;

public class NegativeGameEventController : ApiBaseController
{
    private readonly INegativeGameEventService _negativeGameEventService;

    public NegativeGameEventController(INegativeGameEventService negativeGameEventService)
    {
        _negativeGameEventService = negativeGameEventService;
    }

    //[Authorize(Roles = "Admin")]
    [HttpGet("negativeGameEvents/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _negativeGameEventService.GetAsync(id);
        return Ok(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpGet("negativeGameEvents")]
    public async Task<IActionResult> Find()
    {
        var result = await _negativeGameEventService.FindAsync();
        return Ok(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("negativeGameEvents")]
    public async Task<IActionResult> Create(NegativeGameEventResult negativeGameEventResult)
    {
        var result = await _negativeGameEventService.Create(negativeGameEventResult);
        return Ok(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut("negativeGameEvents/{id}")]
    public async Task<IActionResult> Update(int id, NegativeGameEventResult negativeGameEventResult)
    {
        var result = await _negativeGameEventService.Update(id, negativeGameEventResult);
        return Ok(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("negativeGameEvents/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _negativeGameEventService.DeleteAsync(id);
        return Ok(result);
    }
}