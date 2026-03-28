using Microsoft.EntityFrameworkCore;
using TestRepo.Repository;

namespace TestRepo.Service.User;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateUser(Request.UserRequest request)
    {
        var existingEmail = _dbContext.Users.Where(x => x.Email == request.Email);
        var isExistedEmail = await existingEmail.AnyAsync();
        if (isExistedEmail)
            throw new Exception("Email has been used");

        var newUser = new Repository.Entity.User()
        {
            Email = request.Email,
            Password = request.Password,
            Role = "User"
        };
        
        _dbContext.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return "User created";
    }

    public async Task<Base.Response.PageResult<Response.PageResponse>> GetUsers(string? searchTerm, int pageSize, int pageIndex)
    {
        var query = _dbContext.Users.Where(x => true);
        if (searchTerm != null)
        {
            query = query.Where(x => x.Email.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.Email);
        
        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        var selectedQuery = query.Select(x => new Response.PageResponse()
        {
            Email = x.Email,
            Password = x.Password,
            Role = x.Role
        });
        
        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count;

        var result = new Base.Response.PageResult<Response.PageResponse>()
        {
            Items = listResult,
            TotalItems = totalItems,
            PageSize = pageSize,
            PageIndex = pageIndex
        };
        
        return result;

    }
}