using Microsoft.AspNetCore.Mvc;
using TestRepo.Service.User;

namespace TestRepo.Api.Controller;

[ApiController]  
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IService _userService;
    public UserController(IService userService)
    {
        _userService = userService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUser(Request.UserRequest request)
    {
        var user = await _userService.CreateUser(request);
        return Ok(user);
    }
}