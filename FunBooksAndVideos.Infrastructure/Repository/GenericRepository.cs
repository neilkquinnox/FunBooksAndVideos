using FunBooksAndVideos.WebApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Infrastructure.Repository
{
    public class GenericRepository<T> : DbContext, IGenericRepository<T>
     where T : class, IEntity
    {
        private readonly DatabaseContext _databaseContext;

        public GenericRepository(DatabaseContext databaseContext)

        {
            _databaseContext = databaseContext;
        }

        public IQueryable<T> GetAll()
        {
            return _databaseContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(int id)
        {
            return await _databaseContext.Set<T>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id.ToString());
        }

        public async Task Create(T entity)
        {
            await _databaseContext.Set<T>().AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(int id, T entity)
        {
            _databaseContext.Set<T>().Update(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _databaseContext.Set<T>().Remove(entity);
            await _databaseContext.SaveChangesAsync();
        }
    }
}