using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LinkShortener.Persistence.Database.ShortenedLink
{
    public partial class ShortenedLinkDBContext : DbContext
    {
        public ShortenedLinkDBContext()
        {
        }

        public ShortenedLinkDBContext(DbContextOptions<ShortenedLinkDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ShortenedLink> ShortenedLink { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:localDBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedLink>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.OriginalUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShortUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
