using Microsoft.EntityFrameworkCore;
using StandingOrders.API.Entities;
using StandingOrders.API.Maps;
using StandingOrders.API.Models.Entities;
using StandingOrders.API.Models.Entities.Maps;

namespace StandingOrders.API.Contexts
{
    public class IB_SampleContext : DbContext
    {
        
        public IB_SampleContext(DbContextOptions<IB_SampleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StandingOrderMap());
            modelBuilder.ApplyConfiguration(new IntervalMap());
            modelBuilder.ApplyConfiguration(new ConstantSymbolMap());
        }

    }
}
