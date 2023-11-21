using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using System.Linq.Expressions;

namespace SchoolProject.Infrustructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Remove(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsQueryable();
            Query = Query.Where(filter);
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }

            return await Query.FirstOrDefaultAsync();
        }

        public void RemoveRange(IEnumerable<T> Entities)
        {
            _dbContext.Set<T>().RemoveRange(Entities);
        }

        public async Task<IEnumerable<T>> GetAllAsTracking(string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsQueryable();
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }

            return await Query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsNoTracking(string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsNoTracking().AsQueryable();
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }

            return await Query.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            var RowsEfected = await _dbContext.SaveChangesAsync();
            return RowsEfected > 0 ? true : false;
        }

        public async Task<bool> IsExist(int id)
        {
            var StudnetResult = await _dbContext.Set<T>().FindAsync(id);

            return StudnetResult is null ? false : true;
        }

        public void UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void ClearChangeTracking()
        {
            _dbContext.ChangeTracker.Clear();
        }
    }
}