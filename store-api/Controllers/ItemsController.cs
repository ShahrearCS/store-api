using StoreApi.Models;
using StoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace StoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly StoreService _storeService;

    public ItemsController(StoreService storeService) =>
        _storeService = storeService;

    [HttpGet]
    public async Task<List<Item>> Get() =>
        await _storeService.GetAllItemsAsync();

    [HttpGet("{_id:length(24)}")]
    public async Task<ActionResult<Item>> Get(string _id)
    {
        var item = await _storeService.GetItemAsync(_id);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Item newItem)
    {
        await _storeService.CreateItemAsync(newItem);

        return CreatedAtAction(nameof(Get), new { _id = newItem._id }, newItem);
    }

    [HttpPut("{_id:length(24)}")]
    public async Task<IActionResult> Update(string _id, Item updatedItem)
    {
        var item = await _storeService.GetItemAsync(_id);

        if (item is null)
        {
            return NotFound();
        }

        updatedItem._id = item._id;

        await _storeService.UpdateItemAsync(_id, updatedItem);

        return NoContent();
    }

    [HttpDelete("{_id:length(24)}")]
    public async Task<IActionResult> Delete(string _id)
    {
        var item = await _storeService.GetItemAsync(_id);

        if (item is null)
        {
            return NotFound();
        }

        await _storeService.RemoveItemAsync(_id);

        return NoContent();
    }
}