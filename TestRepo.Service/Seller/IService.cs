namespace TestRepo.Service.Seller;

public interface IService
{
    public Task<string> CreateSeller(Request.SellerRequest request);   
}