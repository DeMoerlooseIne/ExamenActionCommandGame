using ActionCommandGame.Api.Authentication.Extensions;
using ActionCommandGame.Services.Abstractions;
using ActionCommandGame.Services.Model.Results;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.Api.Controllers
{
    public class ItemsController : ApiBaseController
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _itemService.GetAsync(id, null);
            return Ok(result);
        }

        [HttpGet("items")]
        public async Task<IActionResult> Find()
        {
            var result = await _itemService.FindAsync(User.GetId());
            return Ok(result);
        }

        [HttpPost("items")]
        public async Task<IActionResult> Create(ItemResult item)
        {
            var result = await _itemService.Create(item);
            return Ok(result);
        }

        [HttpPut("items/{id}")]
        public async Task<IActionResult> Update(int id, ItemResult item)
        {
            var result = await _itemService.Update(id, item);
            return Ok(result);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _itemService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
