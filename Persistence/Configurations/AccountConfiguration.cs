using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAddOrUpdate();
            builder.Property(p => p.AccountType).HasMaxLength(50);
            builder.Property(p => p.DateCreated).IsRequired();
        }
    }
}
