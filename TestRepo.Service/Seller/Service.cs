using Microsoft.EntityFrameworkCore;
using TestRepo.Repository;

namespace TestRepo.Service.Seller;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> CreateSeller(Request.SellerRequest request)
    {
        var existingEmail = _dbContext.Sellers.Where(x => x.User.Email == request.Email);
        var isExistedEmail = await existingEmail.AnyAsync();
        if (isExistedEmail)
            throw new Exception("Email has been used");

        var newUser = new Repository.Entity.User()
        {
            Email = request.Email,
            Password = request.Password,
            Role = "Seller"
        };
        _dbContext.Add(newUser);
        await _dbContext.SaveChangesAsync();

        var newSeller = new Repository.Entity.Seller()
        {
            TaxCode = request.TaxCode,
            CompanyAddress = request.CompanyAddress,
            CompanyName = request.CompanyName,
            UserId = newUser.Id,
        };
        _dbContext.Add(newSeller);
        await _dbContext.SaveChangesAsync();
        return "Add Seller created";
    }
}