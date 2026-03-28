namespace TestRepo.Service.User;

public class Request
{
    public class UserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}