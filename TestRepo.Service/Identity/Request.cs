namespace TestRepo.Service.Identity;

public class Request
{
    public class IdentityRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    
}