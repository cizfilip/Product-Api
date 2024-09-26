using Microsoft.EntityFrameworkCore;
using ProductAPI.Database;

namespace ProductAPI.Tests.IntegrationTests;

public class CrudRepositoryTests : IDisposable
{
    private readonly ProductsDbContext _context;
    private readonly CrudRepository<Product> _repository;

    public CrudRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase("ProductsTest")
            .Options;

        _context = new ProductsDbContext(options);
        _context.Database.EnsureCreated();

        _repository = new CrudRepository<Product>(_context);
    }

    [Fact]
    public async Task GetAll_ShouldReturnPagedProducts()
    {
        // Arrange
        await DatabaseSeeder.SeedProducts(_context, 50);

        // Act
        var products = await _repository.GetAll(2, 10);

        // Assert
        Assert.Equal(2, products.PageNumber);
        Assert.Equal(10, products.PageSize);
        Assert.Equal(10, products.Data.Count);
    }

    [Fact]
    public async Task GetById_ShouldReturnProducts()
    {
        // Arrange
        await DatabaseSeeder.SeedProducts(_context, 1);
        var expectedProduct = await _context.Products.FirstAsync();

        // Act
        var product = await _repository.GetById(expectedProduct.Id);

        // Assert
        Assert.Equivalent(expectedProduct, product);
    }

    [Fact]
    public async Task Add_ShouldAddProduct()
    {
        // Arrange
        var product = new Product
        {
            Name = "Test product",
            Price = 100
        };

        // Act
        await _repository.Add(product);

        // Assert
        var actualProduct = await _context.Products.FirstAsync();

        Assert.Equal(product.Name, actualProduct.Name);
        Assert.Equal(product.Price, actualProduct.Price);
        Assert.NotEqual(Guid.Empty, actualProduct.Id);
    }

    [Fact]
    public async Task Update_ShouldUpdateProduct()
    {
        // Arrange
        await DatabaseSeeder.SeedProducts(_context, 1);
        var productBeforeUpdate = await _context.Products.AsNoTracking().FirstAsync();

        // Act
        var productUpdate = new Product
        {
            Id = productBeforeUpdate.Id,
            Name = "Updated product",
            Price = 100,
        };
        await _repository.Update(productUpdate);

        // Assert
        var updatedProduct = await _context.Products.FirstAsync();

        Assert.Equal(productUpdate.Id, updatedProduct.Id);
        Assert.Equal(productUpdate.Name, updatedProduct.Name);
        Assert.Equal(productUpdate.Price, updatedProduct.Price);
        
        Assert.NotEqual(productBeforeUpdate.Name, updatedProduct.Name);
        Assert.NotEqual(productBeforeUpdate.Price, updatedProduct.Price);
    }

    [Fact]
    public async Task Delete_ShouldDeleteProduct()
    {
        // Arrange
        await DatabaseSeeder.SeedProducts(_context, 1);
        var productToDelete = await _context.Products.AsNoTracking().FirstAsync();

        // Act
        await _repository.Delete(productToDelete);

        // Assert
        var products = await _context.Products.ToListAsync();

        Assert.Empty(products);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
