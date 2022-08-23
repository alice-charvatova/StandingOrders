using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StandingOrders.API.Entities;

namespace StandingOrders.API.Maps
{
    public class StandingOrderMap : IEntityTypeConfiguration<StandingOrder>
    {
        public void Configure(EntityTypeBuilder<StandingOrder> builder)
        {
            builder.ToTable("StandingOrders");
            builder.HasKey(s => s.StandingOrderId);
            builder.Property(s => s.StandingOrderId)
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Amount).HasColumnType("decimal(8,2)");
            builder.Property(s => s.Name).HasMaxLength(512);
            builder.Property(s => s.AccountNumber).HasMaxLength(512);
            builder.Property(s => s.Note).HasMaxLength(255);
            builder.Property(s => s.VariableSymbol).HasMaxLength(10);
            builder.Property(s => s.SpecificSymbol).HasMaxLength(10);
            builder.Property(s => s.ConstantSymbol).HasMaxLength(10);
            builder.Property(s => s.IntervalId).HasColumnName("Interval");
            builder.HasOne(x => x.Interval).WithMany().HasForeignKey(x => x.IntervalId);
        }
    }
}
