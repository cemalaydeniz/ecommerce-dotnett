using AutoMapper;
using ecommerce_dotnet.Data;
using ecommerce_dotnet.Mappings;
using ecommerce_dotnet.Middlewares;
using ecommerce_dotnet.Models;
using ecommerce_dotnet.Services.Implementation;
using ecommerce_dotnet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var autoMapper = new MapperConfiguration(_ =>
{
    _.AddProfile(new ProductMapping());
});

//~ Begin - Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(_ => _.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.Parse("8.0.29-mysql")));

builder.Services.AddIdentity<User, Role>(_ =>
{
    _.Password.RequireDigit = true;
    _.Password.RequiredLength = 6;
    _.Password.RequireNonAlphanumeric = false;
    _.Password.RequireUppercase = false;

    _.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerSupportService, CustomerSupportService>();
builder.Services.AddSingleton(autoMapper.CreateMapper());
//~ End

var app = builder.Build();

// Initialize database
await new InitializeDb(app.Services).InitializeRoles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<InternalErrorHandler>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
