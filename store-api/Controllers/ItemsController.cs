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
    public async Task<IActionResult> Create(Item newItem)
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

    //waitlist
    [HttpGet("waitlist/{_id:length(24)}")]
    public async Task<IActionResult> GetWaitlist(string _id)
    {
        var item = await _storeService.GetItemAsync(_id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item.waitList);
    }

    [HttpPut("waitlist/{_id:length(24)}")]
    public async Task<IActionResult> AddWaitList(string _id, String userId)
    {
        var item = await _storeService.GetItemAsync(_id);
        var user = await _storeService.GetUserAsync(userId);
        if (item is null || user is null)
        {
            return NotFound();
        }

        if (item.waitList is null)
        {
            item.waitList = new List<User>();
            item.waitList.Add(user);
        }
        else
        {
            //if the user already exists
            if (item.waitList.Find(user => user._id == userId) != null)
            {
                return NotFound();
            }
            else
            {
                item.waitList.Add(user);
            }
        }

        await _storeService.UpdateItemAsync(_id, item);

        return Ok(item.waitList);
    }

    [HttpDelete("waitlist/{_id:length(24)}")]
    public async Task<IActionResult> RemoveWaitList(string _id, String userId)
    {
        var item = await _storeService.GetItemAsync(_id);

        if (item is null || item.waitList == null)
        {
            return NotFound();
        }

        item.waitList.RemoveAll(user => user._id == userId);
        await _storeService.UpdateItemAsync(_id, item);

        return Ok(item.waitList);
    }
}