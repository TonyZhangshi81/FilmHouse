using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class FindPasswordConfiguration : IEntityTypeConfiguration<FindPasswordEntity>
{
    public void Configure(EntityTypeBuilder<FindPasswordEntity> builder)
    {
        builder.ToTable("FindPwds");

        builder.HasKey(e => new { e.FindId }).HasName("findpwds_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.FindPwds)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_FindPwd_UserAccount");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.FindId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<FindIdVO.FindIdVOValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.Token)
            .IsRequired()
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .HasConversion<TokenStringVO.TokenStringVOValueConverter>();

        builder.Property(e => e.EmailAddress)
           .IsRequired()
           .HasColumnType("varchar(256)")
           .HasMaxLength(256)
           .HasConversion<EmailAddressVO.EmailAddressVOValueConverter>();

        builder.Property(e => e.ExpiryTime)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<ExpiryTimeVO.ExpiryTimeVOValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("boolean")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}