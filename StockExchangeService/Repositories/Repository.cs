using Microsoft.EntityFrameworkCore;
using StockExchangeService.Data;

namespace StockExchangeService.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly StockDataDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(StockDataDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>>? GetListAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
