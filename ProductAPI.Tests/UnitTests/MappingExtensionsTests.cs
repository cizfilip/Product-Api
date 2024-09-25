using ProductAPI.Database;
using ProductAPI.Products;

namespace ProductAPI.Tests.UnitTests;

public class MappingExtensionsTests
{
    [Fact]
    public void ToEntity_FromDto_MapsCorrectly()
    {
        // Arrange
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Price = 10
        };

        // Act
        var entity = dto.ToEntity();

        // Assert
        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Name, entity.Name);
        Assert.Equal(dto.Price, entity.Price);
    }

    [Fact]
    public void ToDto_FromEntity_MapsCorrectly()
    {
        // Arrange
        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Price = 10
        };

        // Act
        var dto = entity.ToDto();

        // Assert
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Name, dto.Name);
        Assert.Equal(entity.Price, dto.Price);
    }
}
