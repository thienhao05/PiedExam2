namespace TestRepo.Service.User;

public class Response
{
    public class PageResponse
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; }
    }
}