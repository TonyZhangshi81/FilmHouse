using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class UserAccountConfiguration : IEntityTypeConfiguration<UserAccountEntity>
{
    public void Configure(EntityTypeBuilder<UserAccountEntity> builder)
    {
        builder.HasKey(e => new { e.Account }).HasName("user_account_ix00");

        builder.ToTable("UserAccount");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(e => e.Account)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.Password)
            .IsRequired()
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);

        builder.Property(e => e.EmailAddress)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.Avatar)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.Cover)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.IsAdmin)
            .HasDefaultValue(false)
            .HasColumnType("tinyint");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime(3)");

    }
}