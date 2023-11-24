using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AskConfiguration : IEntityTypeConfiguration<AskEntity>
{
    public void Configure(EntityTypeBuilder<AskEntity> builder)
    {
        builder.HasKey(e => new { e.AskId }).HasName("ask_ix00");

        builder.ToTable("Ask");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.AskId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<AskIdVO.AskIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.RequestTime)
            .HasColumnType("timestamp(3)")
            .HasConversion<RequestTimeVO.RequestTimeValueConverter>();

        builder.Property(e => e.RequestWith)
            .HasColumnType("int")
            .HasConversion<RequestWithVO.RequestWithValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("text")
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.Status)
            .HasDefaultValue(false)
            .HasColumnType("boolean");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}