using System.Linq.Expressions;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsTracking(string[] InclueProperties = null);

        Task<IEnumerable<T>> GetAllAsNoTracking(string[] InclueProperties = null);

        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string[] InclueProperties = null);

        Task Add(T entity);

        void Remove(T Entity);

        void RemoveRange(IEnumerable<T> Entities);
    }
}