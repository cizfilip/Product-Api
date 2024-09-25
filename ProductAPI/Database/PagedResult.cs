namespace ProductAPI.Database;

public class PagedResult<TEntity>
{
    public PagedResult(IReadOnlyList<TEntity> data, int pageNumber, int pageSize, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }

    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalRecords { get; init; }
    public IReadOnlyList<TEntity> Data { get; init; }
}
