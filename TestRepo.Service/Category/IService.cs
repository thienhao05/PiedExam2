namespace TestRepo.Service.Category;

public interface IService
{
    public Task<string> CreateCategory(Request.CategoryRequest request);
    
    public Task<List<Response.CategoryResponse>> GetCategories();
    public Task<List<Response.CategoryResponse>> GetAllCategories(Guid parentId);
}