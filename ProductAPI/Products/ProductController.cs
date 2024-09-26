using Microsoft.AspNetCore.Mvc;
using ProductAPI.Database;
using ProductAPI.Products;

namespace ProductAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ICrudRepository<Product> _repository;

    public ProductController(ILogger<ProductController> logger, ICrudRepository<Product> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType<PagedResult<ProductDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            return BadRequest($"{nameof(pageNumber)} and {nameof(pageSize)} size must be greater than 0.");

        var pagedProducts = await _repository.GetAll(pageNumber, pageSize);

        return Ok(pagedProducts.ToPagedDto());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var product = await _repository.GetById(id);

        if (product is null)
        {
            return NotFound();
        }

        return product.ToDto();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductDto product)
    {
        var productEntity = product.ToEntity();

        await _repository.Add(productEntity);

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = productEntity.Id },
            productEntity.ToDto());
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutProduct(Guid id, ProductDto product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        var productEntity = await _repository.GetById(id);
        if (productEntity is null)
        {
            return NotFound();
        }

        await _repository.Update(product.ToEntity());

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _repository.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        await _repository.Delete(product);
        
        return NoContent();
    }
}
