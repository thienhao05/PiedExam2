namespace TestRepo.Service.User;

public interface IService
{
    public Task<string> CreateUser(Request.UserRequest request);

    public Task<Base.Response.PageResult<Response.PageResponse>> GetUsers(string? searchTerm, int pageSize,
        int pageIndex);
}