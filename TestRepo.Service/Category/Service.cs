using Microsoft.EntityFrameworkCore;
using TestRepo.Repository;

namespace TestRepo.Service.Category;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateCategory(Request.CategoryRequest request)
    {
        var existingCate = _dbContext.Categories.Where(x => x.Name == request.Name);
        var isExistingCate = await existingCate.AnyAsync();

        if (isExistingCate)
            throw new Exception("Category has been used");

        var newCate = new Repository.Entity.Category()
        {
            Name = request.Name,
            ParentId = request.ParentId,
        };

        _dbContext.Add(newCate);
        await _dbContext.SaveChangesAsync();
        return "Category created";
    }

    public async Task<List<Response.CategoryResponse>> GetCategories()
    {
        var query = _dbContext.Categories.Where(x => true);
        query = query.OrderBy(x => x.Name);

        var selectedQuery = query.Select(x => new Response.CategoryResponse()
        {
            Id = x.Id,
            Name = x.Name,
        });

        var result = await selectedQuery.ToListAsync();
        return result;
    }

    public async Task<List<Response.CategoryResponse>> GetAllCategories(Guid parentId)
    {
        var query = _dbContext.Categories.Where(x => x.ParentId ==  parentId);
        query = query.OrderBy(x => x.Name);

        var selectedQuery = query.Select(x => new Response.CategoryResponse()
        {
            Id = x.Id,
            Name = x.Name,
        });

        var result = await selectedQuery.ToListAsync();
        return result;
    }
}