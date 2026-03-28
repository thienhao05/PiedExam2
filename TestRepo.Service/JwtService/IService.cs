using System.Security.Claims;

namespace TestRepo.Service.JwtService;

public interface IService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    
    ClaimsPrincipal ValidateToken(string token); 

}