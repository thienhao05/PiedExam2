using Microsoft.EntityFrameworkCore;
using TestRepo.Api.Extensions;
using TestRepo.Api.Middlewares;
using TestRepo.Repository;
using JwtService = TestRepo.Service.JwtService;
using CategoryService = TestRepo.Service.Category;
using UserService = TestRepo.Service.User;
using IdentityService = TestRepo.Service.Identity;
using SellerService = TestRepo.Service.Seller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(  
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddSwaggerServices();

builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
builder.Services.AddScoped<CategoryService.IService, CategoryService.Service>();
builder.Services.AddScoped<UserService.IService, UserService.Service>();
builder.Services.AddScoped<IdentityService.IService, IdentityService.Service>();
builder.Services.AddScoped<SellerService.IService, SellerService.Service>();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

// Cuộc đời là một chuỗi các sự kiện,
    // và chúng ta chỉ là những người diễn viên trong đó.
// Hãy sống hết mình và tận hưởng từng khoảnh khắc!

// Không ai mong các em pass và thành công hơn chính anh
    // Vậy nên hãy cố gắng trở thành phiên bản tốt nhất nhé

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAPI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();