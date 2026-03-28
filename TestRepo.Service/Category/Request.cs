namespace TestRepo.Service.Category;

public class Request
{
    public class CategoryRequest
    {
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}