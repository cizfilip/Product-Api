namespace ProductAPI.Database;

public class Product : IEntity
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required int Price { get; set; }
}
