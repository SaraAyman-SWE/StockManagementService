namespace StockExchangeService.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>>? GetListAsync();
        Task InsertAsync(TEntity entity);
        Task SaveAsync();
    }
}
