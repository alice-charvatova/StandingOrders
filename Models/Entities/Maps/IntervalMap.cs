using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StandingOrders.API.Models.Entities;

namespace StandingOrders.API.Maps
{
    public class IntervalMap : IEntityTypeConfiguration<Interval>
    {
        public void Configure(EntityTypeBuilder<Interval> builder)
        {
            builder.ToTable("Interval");
            builder.HasKey(i => i.IntervalId);
            builder.Property(i => i.IntervalId).ValueGeneratedOnAdd();
            builder.Property(i => i.Value).HasMaxLength(50);
        }     
    }
}
