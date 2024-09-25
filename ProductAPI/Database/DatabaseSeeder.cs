using Bogus;

namespace ProductAPI.Database;

internal static class DatabaseSeeder
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();

        await dbContext.Database.EnsureCreatedAsync();
        await SeedProducts(dbContext);
    }
    
    private static async Task SeedProducts(ProductsDbContext dbContext)
    {
        var productFaker = new Faker<Product>()
            .RuleFor(x => x.Id, f => f.Random.Uuid())
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Price, f => f.Random.Int(10, 1_000_000));

        var products = productFaker.Generate(50);

        await dbContext.AddRangeAsync(products);
        await dbContext.SaveChangesAsync();
    }
}
