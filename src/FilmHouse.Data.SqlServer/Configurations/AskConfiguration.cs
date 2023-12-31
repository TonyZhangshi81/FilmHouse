using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AskConfiguration : IEntityTypeConfiguration<AskEntity>
{
    public void Configure(EntityTypeBuilder<AskEntity> builder)
    {
        builder.ToTable("Ask");

        builder.HasKey(e => new { e.AskId });
        builder.HasAnnotation("SqlServer:Name", "ask_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Asks)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Ask_UserAccount")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Asks)
            .HasForeignKey(d => d.MovieId)
            .HasConstraintName("FK_Ask_Movie");


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.AskId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<AskIdVO.AskIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.RequestTime)
            .HasColumnType("datetime")
            .HasConversion<RequestTimeVO.RequestTimeValueConverter>();

        builder.Property(e => e.RequestWith)
            .HasDefaultValue(typeof(RequestWithVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(10, 0)")
            .HasConversion<RequestWithVO.RequestWithValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.Status)
            .HasDefaultValue(typeof(AskStatusVO).CreateValueObjectInstance("false"))
            .HasColumnType("bit")
            .HasConversion<AskStatusVO.AskStatusValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("bit")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();


    }
}