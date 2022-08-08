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

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Companion> Companions { get; set; } = null!;
        public virtual DbSet<FoodElement> FoodElements { get; set; } = null!;
        public virtual DbSet<FoodInventory> FoodInventories { get; set; } = null!;
        public virtual DbSet<FoodStat> FoodStats { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Species> Species { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments", "WALS_P2");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.PostIdFk).HasColumnName("postId_fk");

                entity.Property(e => e.UserIdFk).HasColumnName("userId_fk");

                entity.HasOne(d => d.PostIdFkNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comments__postId__5BAD9CC8");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comments__userId__5AB9788F");
            });

            modelBuilder.Entity<Companion>(entity =>
            {
                entity.ToTable("companions", "WALS_P2");

                entity.Property(e => e.CompanionId).HasColumnName("companionId");

                entity.Property(e => e.CompanionBirthday)
                    .HasColumnType("datetime")
                    .HasColumnName("companion_birthday");

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
                    .HasConstraintName("FK__companion__speci__3C34F16F");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.Companions)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__companion__user___3B40CD36");
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
                    .HasName("PK__foodInve__410828EC3FBC3CF3");

                entity.ToTable("foodInventory", "WALS_P2");

                entity.Property(e => e.UserIdFk).HasColumnName("userId_fk");

                entity.Property(e => e.FoodStatsIdFk).HasColumnName("foodStatsId_fk");

                entity.Property(e => e.FoodCount).HasColumnName("foodCount");

                entity.HasOne(d => d.FoodStatsIdFkNavigation)
                    .WithMany(p => p.FoodInventories)
                    .HasForeignKey(d => d.FoodStatsIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__foodInven__foodS__208CD6FA");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.FoodInventories)
                    .HasForeignKey(d => d.UserIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__foodInven__userI__1F98B2C1");
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

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.RelationshipId)
                    .HasName("PK__friends__4BCCCED70F129545");

                entity.ToTable("friends", "WALS_P2");

                entity.Property(e => e.RelationshipId).HasColumnName("relationshipId");

                entity.Property(e => e.Status)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserIdFrom).HasColumnName("userId_from");

                entity.Property(e => e.UserIdTo).HasColumnName("userId_to");

                entity.HasOne(d => d.UserIdFromNavigation)
                    .WithMany(p => p.FriendUserIdFromNavigations)
                    .HasForeignKey(d => d.UserIdFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__friends__userId___4F47C5E3");

                entity.HasOne(d => d.UserIdToNavigation)
                    .WithMany(p => p.FriendUserIdToNavigations)
                    .HasForeignKey(d => d.UserIdTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__friends__userId___503BEA1C");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts", "WALS_P2");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.Content)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.UserIdFk).HasColumnName("userId_fk");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__posts__userId_fk__540C7B00");
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
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("speciesName");

                entity.HasOne(d => d.FoodElementIdFkNavigation)
                    .WithMany(p => p.Species)
                    .HasForeignKey(d => d.FoodElementIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__species__foodEle__37703C52");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "WALS_P2");

                entity.HasIndex(e => e.Username, "UQ__users__F3DBC572CF9010EE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.AccountAge)
                    .HasColumnType("datetime")
                    .HasColumnName("account_age");

                entity.Property(e => e.EggCount).HasColumnName("eggCount");

                entity.Property(e => e.EggTimer)
                    .HasColumnType("datetime")
                    .HasColumnName("eggTimer");

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
                        l => l.HasOne<Post>().WithMany().HasForeignKey("PostIdFk").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__likes__postId_fk__57DD0BE4"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserIdFk").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__likes__userId_fk__56E8E7AB"),
                        j =>
                        {
                            j.HasKey("UserIdFk", "PostIdFk").HasName("PK__likes__20D2FDE6EE730DCD");

                            j.ToTable("likes", "WALS_P2");

                            j.IndexerProperty<int>("UserIdFk").HasColumnName("userId_fk");

                            j.IndexerProperty<int>("PostIdFk").HasColumnName("postId_fk");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
