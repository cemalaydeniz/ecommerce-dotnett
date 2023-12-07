using ecommerce_dotnet.Data;
using ecommerce_dotnet.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
//~ End

var app = builder.Build();

// Initialize database
await new InitializeDb(app.Services).InitializeRoles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
