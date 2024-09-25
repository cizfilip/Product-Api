using ProductAPI.Database;

namespace ProductAPI.Products;

public static class MappingExtensions
{
    public static ProductDto ToDto(this Product product) => new ProductDto
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
    };

    public static PagedResult<ProductDto> ToPagedDto(this PagedResult<Product> pagedProducts) => new PagedResult<ProductDto>(
            pagedProducts.Data.Select(x => x.ToDto()).ToList(),
            pagedProducts.PageNumber,
            pagedProducts.PageSize,
            pagedProducts.TotalRecords
        );

    public static Product ToEntity(this ProductDto product) => new Product
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
    };
}
