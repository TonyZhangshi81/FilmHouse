using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class UserAccountConfiguration : IEntityTypeConfiguration<UserAccountEntity>
{
    public void Configure(EntityTypeBuilder<UserAccountEntity> builder)
    {
        builder.ToTable("UserAccount");

        builder.HasKey(e => new { e.UserId }).HasName("user_account_ix00");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.Account)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<AccountNameVO.AccountNameValueConverter>();

        builder.Property(e => e.PasswordHash)
            .IsRequired()
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .HasConversion<PasswordHashVO.PasswordHashVOValueConverter>();

        builder.Property(e => e.EmailAddress)
            .IsRequired()
            .HasColumnType("varchar(256)")
            .HasMaxLength(256)
            .HasConversion<EmailAddressVO.EmailAddressVOValueConverter>();

        builder.Property(e => e.Avatar)
            .HasColumnType("varchar(256)")
            .HasMaxLength(256)
            .HasConversion<UserAvatarVO.UserAvatarVOValueConverter>();

        builder.Property(e => e.Cover)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasConversion<CoverVO.CoverValueConverter>();

        builder.Property(e => e.IsAdmin)
            .HasDefaultValue(typeof(IsAdminVO).CreateValueObjectInstance("false"))
            .HasColumnType("tinyint")
            .HasConversion<IsAdminVO.IsAdminVOValueConverter>();

        builder.Property(e => e.LastLoginIp)
            .HasColumnType("varchar(64)")
            .HasMaxLength(64)
            .HasConversion<LastLoginIpVO.LastLoginIpVOValueConverter>();

        builder.Property(e => e.LastLoginTime)
            .HasColumnType("datetime(3)")
            .HasConversion<LastLoginTimeVO.LastLoginTimeVOValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("tinyint")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}