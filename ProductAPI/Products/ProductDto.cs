namespace ProductAPI.Products;

public class ProductDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required int Price { get; set; }
}
