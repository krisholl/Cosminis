using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Entities
{
    public partial class wearelosingsteamContext : DbContext
    {
        public wearelosingsteamContext()
        {
        }

        public wearelosingsteamContext(DbContextOptions<wearelosingsteamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Companion> Companions { get; set; } = null!;
        public virtual DbSet<FoodElement> FoodElements { get; set; } = null!;
        public virtual DbSet<FoodInventory> FoodInventories { get; set; } = null!;
        public virtual DbSet<FoodStat> FoodStats { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Species> Species { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companion>(entity =>
            {
                entity.HasKey(e => e.CreatureId)
                    .HasName("PK__companio__9F2E25E06ADEABA4");

                entity.ToTable("companions", "WALS_P2");

                entity.Property(e => e.CreatureId).HasColumnName("creatureId");

                entity.Property(e => e.Hunger).HasColumnName("hunger");

                entity.Property(e => e.Mood)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("mood");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nickname");

                entity.Property(e => e.SpeciesFk).HasColumnName("species_fk");

                entity.Property(e => e.UserFk).HasColumnName("user_fk");

                entity.HasOne(d => d.SpeciesFkNavigation)
                    .WithMany(p => p.Companions)
                    .HasForeignKey(d => d.SpeciesFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__companion__speci__7C4F7684");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.Companions)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__companion__user___7B5B524B");
            });

            modelBuilder.Entity<FoodElement>(entity =>
            {
                entity.ToTable("foodElement", "WALS_P2");

                entity.Property(e => e.FoodElementId).HasColumnName("foodElementId");

                entity.Property(e => e.FoodElement1)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("foodElement");
            });

            modelBuilder.Entity<FoodInventory>(entity =>
            {
                entity.HasKey(e => new { e.UserIdFk, e.FoodStatsIdFk })
                    .HasName("PK__foodInve__410828EC78FD027E");

                entity.ToTable("foodInventory", "WALS_P2");

                entity.Property(e => e.UserIdFk).HasColumnName("userId_fk");

                entity.Property(e => e.FoodStatsIdFk).HasColumnName("foodStatsId_fk");

                entity.Property(e => e.FoodCount).HasColumnName("foodCount");

                entity.HasOne(d => d.FoodStatsIdFkNavigation)
                    .WithMany(p => p.FoodInventories)
                    .HasForeignKey(d => d.FoodStatsIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__foodInven__foodS__03F0984C");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.FoodInventories)
                    .HasForeignKey(d => d.UserIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__foodInven__userI__02FC7413");
            });

            modelBuilder.Entity<FoodStat>(entity =>
            {
                entity.HasKey(e => e.FoodStatsId)
                    .HasName("PK__foodStat__0E4BD16BAEF0F536");

                entity.ToTable("foodStats", "WALS_P2");

                entity.Property(e => e.FoodStatsId).HasColumnName("foodStatsId");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FoodElementFk).HasColumnName("foodElement_fk");

                entity.Property(e => e.FoodName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("foodName");

                entity.Property(e => e.HungerRestore).HasColumnName("hungerRestore");

                entity.HasOne(d => d.FoodElementFkNavigation)
                    .WithMany(p => p.FoodStats)
                    .HasForeignKey(d => d.FoodElementFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__foodStats__foodE__00200768");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts", "WALS_P2");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.UserIdFk).HasColumnName("userId_fk");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__posts__userId_fk__0A9D95DB");
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.ToTable("species", "WALS_P2");

                entity.Property(e => e.SpeciesId).HasColumnName("speciesId");

                entity.Property(e => e.BaseDex).HasColumnName("baseDex");

                entity.Property(e => e.BaseInt).HasColumnName("baseInt");

                entity.Property(e => e.BaseStr).HasColumnName("baseStr");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ElementType)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("elementType");

                entity.Property(e => e.FoodElementIdFk).HasColumnName("foodElementId_fk");

                entity.Property(e => e.SpeciesName)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("speciesName");

                entity.HasOne(d => d.FoodElementIdFkNavigation)
                    .WithMany(p => p.Species)
                    .HasForeignKey(d => d.FoodElementIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__species__foodEle__778AC167");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "WALS_P2");

                entity.HasIndex(e => e.Username, "UQ__users__F3DBC572E0721E93")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.EggCount).HasColumnName("eggCount");

                entity.Property(e => e.EggTimer).HasColumnName("eggTimer");

                entity.Property(e => e.GoldCount).HasColumnName("goldCount");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasMany(d => d.PostIdFks)
                    .WithMany(p => p.UserIdFks)
                    .UsingEntity<Dictionary<string, object>>(
                        "Like",
                        l => l.HasOne<Post>().WithMany().HasForeignKey("PostIdFk").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__likes__postId_fk__123EB7A3"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserIdFk").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__likes__userId_fk__114A936A"),
                        j =>
                        {
                            j.HasKey("UserIdFk", "PostIdFk").HasName("PK__likes__20D2FDE6D67FDAE5");

                            j.ToTable("likes", "WALS_P2");

                            j.IndexerProperty<int>("UserIdFk").HasColumnName("userId_fk");

                            j.IndexerProperty<int>("PostIdFk").HasColumnName("postId_fk");
                        });

                entity.HasMany(d => d.UserIdFk1s)
                    .WithMany(p => p.UserIdFk2s)
                    .UsingEntity<Dictionary<string, object>>(
                        "Friend",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserIdFk1").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__friends__userId___06CD04F7"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserIdFk2").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__friends__userId___07C12930"),
                        j =>
                        {
                            j.HasKey("UserIdFk1", "UserIdFk2").HasName("PK__friends__1E77AD5BD1649136");

                            j.ToTable("friends", "WALS_P2");

                            j.IndexerProperty<int>("UserIdFk1").HasColumnName("userId_fk1");

                            j.IndexerProperty<int>("UserIdFk2").HasColumnName("userId_fk2");
                        });

                entity.HasMany(d => d.UserIdFk2s)
                    .WithMany(p => p.UserIdFk1s)
                    .UsingEntity<Dictionary<string, object>>(
                        "Friend",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserIdFk2").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__friends__userId___07C12930"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserIdFk1").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__friends__userId___06CD04F7"),
                        j =>
                        {
                            j.HasKey("UserIdFk1", "UserIdFk2").HasName("PK__friends__1E77AD5BD1649136");

                            j.ToTable("friends", "WALS_P2");

                            j.IndexerProperty<int>("UserIdFk1").HasColumnName("userId_fk1");

                            j.IndexerProperty<int>("UserIdFk2").HasColumnName("userId_fk2");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
