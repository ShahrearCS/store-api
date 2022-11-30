using StoreApi.Models;
using StoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace StoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersWishListController : ControllerBase
{
    private readonly StoreService _storeService;

    public UsersWishListController(StoreService storeService) =>
        _storeService = storeService;

    [HttpGet]
    public async Task<List<UsersWishList>> Get() =>
        await _storeService.GetAllUsersWishListAsync();

    [HttpGet("{_id:length(24)}")]
    public async Task<ActionResult<UsersWishList>> Get(string _id)
    {
        var UsersWishList = await _storeService.GetUserWishListAsync(_id);

        if (UsersWishList is null)
        {
            return NotFound();
        }

        return UsersWishList;
    }

    [HttpPost]
    public async Task<IActionResult> Create(UsersWishList newUser)
    {
        await _storeService.CreateUserWishListAsync(newUser);

        return CreatedAtAction(nameof(Get), new { _id = newUser._id }, newUser);
    }

    [HttpPut("{_id:length(24)}")]
    public async Task<IActionResult> Update(string _id, UsersWishList updatedUser)
    {
        var UsersWishList = await _storeService.GetUserWishListAsync(_id);

        if (UsersWishList is null)
        {
            return NotFound();
        }

        updatedUser._id = UsersWishList._id;

        await _storeService.UpdateUserWishListAsync(_id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{_id:length(24)}")]
    public async Task<IActionResult> Delete(string _id)
    {
        var UsersWishList = await _storeService.GetUserWishListAsync(_id);

        if (UsersWishList is null)
        {
            return NotFound();
        }

        await _storeService.RemoveUserWishListAsync(_id);

        return NoContent();
    }
}