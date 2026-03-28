using TestRepo.Repository.Abstraction;

namespace TestRepo.Repository.Entity;

public class Product : BaseEntity<Guid>, IAuditableEntity
{
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    
    public Guid SellerId { get; set; }
    public Seller Seller { get; set; }
    
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}