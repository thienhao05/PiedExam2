using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestRepo.Repository;
using TestRepo.Service.JwtService;

namespace TestRepo.Service.Identity;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly JwtOptions _jwtOption = new();
    private readonly JwtService.IService _tokenService;

    public Service(IConfiguration configuration, AppDbContext dbContext, JwtService.IService tokenService)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
        configuration.GetSection(nameof(JwtOptions)).Bind(_jwtOption);
        
    }

    public async Task<Response.IdentityResponse> Login(Request.IdentityRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user == null)
            throw new Exception("User not found");

        if (user.Password != request.Password)
            throw new Exception("Wrong password");

        var claims = new List<Claim>()
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Role", user.Role),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Expired, DateTimeOffset.UtcNow.AddMinutes(_jwtOption.ExpireMinutes).ToString())
        };
        
        var token = _tokenService.GenerateAccessToken(claims);
        var result = new Response.IdentityResponse()
        {
            AccessToken = token,
        };
        return result;
    }
}