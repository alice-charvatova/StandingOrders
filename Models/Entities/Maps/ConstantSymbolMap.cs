using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace StandingOrders.API.Models.Entities.Maps
{
    public class ConstantSymbolMap : IEntityTypeConfiguration<ConstantSymbol>
    {
        public void Configure(EntityTypeBuilder<ConstantSymbol> builder)
        {
            builder.ToTable("ConstantSymbols");
            builder.HasKey(i => i.ConstantSymbolId);
            builder.Property(i => i.ConstantSymbolId).ValueGeneratedOnAdd();
            builder.Property(i => i.ConstantSymbolValue).HasColumnName("ConstantSymbol");
            builder.Property(i => i.ConstantSymbolValue).HasMaxLength(50);
            builder.Property(i => i.Description).HasMaxLength(100);
        }
    }
}
