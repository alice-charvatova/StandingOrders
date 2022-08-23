using Microsoft.EntityFrameworkCore;
using StandingOrders.API.Contexts;
using System.Linq;

namespace StandingOrders.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IB_SampleContext _context;
        private DbSet<T> table;

        public Repository(IB_SampleContext context)
        {
            _context = context;
            table = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public void Create(T entity)
        {
            table.Add(entity);
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
