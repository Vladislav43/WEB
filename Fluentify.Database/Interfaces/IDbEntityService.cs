using Fluentify.Models.Database;
using Fluentify.Database;

namespace Fluentify.Database.Interfaces
{
    public interface IDbEntityService<T> where T : DbItem
    {
        Task<T> GetById(int id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);

        IQueryable<T> GetAll();
    }
}
