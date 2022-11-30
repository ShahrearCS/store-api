using StoreApi.Models;
using StoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace StoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly StoreService _storeService;

    public UsersController(StoreService storeService) =>
        _storeService = storeService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _storeService.GetAllUsersAsync();

    [HttpGet("{_id:length(24)}")]
    public async Task<ActionResult<User>> Get(string _id)
    {
        var User = await _storeService.GetUserAsync(_id);

        if (User is null)
        {
            return NotFound();
        }

        return User;
    }

    [HttpPost]
    public async Task<IActionResult> Create(User newUser)
    {
        await _storeService.CreateUserAsync(newUser);

        return CreatedAtAction(nameof(Get), new { _id = newUser._id }, newUser);
    }

    [HttpPut("{_id:length(24)}")]
    public async Task<IActionResult> Update(string _id, User updatedUser)
    {
        var User = await _storeService.GetUserAsync(_id);

        if (User is null)
        {
            return NotFound();
        }

        updatedUser._id = User._id;

        await _storeService.UpdateUserAsync(_id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{_id:length(24)}")]
    public async Task<IActionResult> Delete(string _id)
    {
        var User = await _storeService.GetUserAsync(_id);

        if (User is null)
        {
            return NotFound();
        }

        await _storeService.RemoveUserAsync(_id);

        return NoContent();
    }
}