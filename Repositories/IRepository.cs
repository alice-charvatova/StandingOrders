using System.Linq;

namespace StandingOrders.API.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        void Create(T entity);

        void Delete(T entity);

        bool Save();
    }
}