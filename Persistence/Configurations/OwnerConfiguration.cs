using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configurations
{
    internal sealed class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable(nameof(Owner));
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(60);
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.Address).HasMaxLength(100);

            builder.HasMany(p => p.Accounts)
                .WithOne()
                .HasForeignKey(account => account.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
