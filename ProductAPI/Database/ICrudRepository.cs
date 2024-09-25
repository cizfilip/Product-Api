namespace ProductAPI.Database;

public interface ICrudRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<PagedResult<TEntity>> GetAll(int pageNumber, int pageSize);

    Task<TEntity?> GetById(Guid id);

    Task Add(TEntity entity);

    Task Update(TEntity entity);

    Task Delete(TEntity entity);
}
