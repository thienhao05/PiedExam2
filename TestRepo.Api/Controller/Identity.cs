using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TestRepo.Service.Identity;

namespace TestRepo.Api.Controller;

[ApiController]  
[Route("[controller]")]
public class Identity  : ControllerBase
{
    private readonly IService _identityService;
    public Identity(IService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Request.IdentityRequest request)
    {
        var result = await _identityService.Login(request);
        return Ok(result);
    }
}