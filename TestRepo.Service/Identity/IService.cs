namespace TestRepo.Service.Identity;

public interface IService
{
    public Task<Response.IdentityResponse> Login(Request.IdentityRequest request);
}