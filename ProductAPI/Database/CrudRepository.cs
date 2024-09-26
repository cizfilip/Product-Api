using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Database;

public class CrudRepository<TEntity> : ICrudRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly ProductsDbContext _dbContext;

    public CrudRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<TEntity>> GetAll(int pageNumber, int pageSize)
    {
        var totalRecords = await _dbContext.Set<TEntity>().AsNoTracking().CountAsync();

        var products = await _dbContext.Set<TEntity>().AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>(products, pageNumber, pageSize, totalRecords);
    }

    public async Task<TEntity?> GetById(Guid id)
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}