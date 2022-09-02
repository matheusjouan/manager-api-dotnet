using Manager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseMySqlIdentityColumn() // Auto incremento (MySQL)
            .HasColumnType("BIGINT");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(180)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(180)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(180)
            .HasColumnName("email")
            .HasColumnType("VARCHAR(180)");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(12)
            .HasColumnName("password")
            .HasColumnType("VARCHAR(12)");
    }
}

