using Microsoft.EntityFrameworkCore;
using ProductAPI.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddDbContext<ProductsDbContext>(opt =>
    opt.UseInMemoryDatabase("Products"));
builder.Services.AddTransient<ICrudRepository<Product>, CrudRepository<Product>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Error handling
app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await DatabaseSeeder.Seed(app.Services);

app.Run();
