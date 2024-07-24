using Avito.Infrastructure;
using Avito.Infrastructure.Store;
using Avito.Logic.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ICategoryStore, CategoryStore>();
builder.Services.AddScoped<IRoleStore, RoleStore>();
builder.Services.AddScoped<IProductStore, ProductStore>();
builder.Services.AddScoped<IUserStore,UserStore>();

// Add services to the container.
string? connection =
    builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AvitoDbContext>(
    options => options.UseNpgsql(connection, b => b.MigrationsAssembly("Avito")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
