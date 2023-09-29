using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TablonAnuncios.Models
{
    public partial class anunzaContext : DbContext
    {
        public anunzaContext()
        {
        }

        public anunzaContext(DbContextOptions<anunzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anuncio> Anuncios { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.ToTable("anuncio");

                entity.HasIndex(e => e.City, "city");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Config)
                    .HasMaxLength(350)
                    .HasColumnName("config");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(160)
                    .HasColumnName("title");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.Anuncios)
                    .HasForeignKey(d => d.City)
                    .HasConstraintName("anuncio_ibfk_1");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasIndex(e => e.Country, "country");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.State, "state");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.State).HasColumnName("state");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city_ibfk_2");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city_ibfk_1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .HasMaxLength(70)
                    .HasColumnName("name");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(10)
                    .HasColumnName("prefix");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.HasIndex(e => e.Anuncio, "anuncio");

                entity.HasIndex(e => e.User, "user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Anuncio).HasColumnName("anuncio");

                entity.Property(e => e.User).HasColumnName("user");

                entity.HasOne(d => d.AnuncioNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Anuncio)
                    .HasConstraintName("notification_ibfk_1");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("notification_ibfk_2");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("state");

                entity.HasIndex(e => e.Country, "country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasMaxLength(70)
                    .HasColumnName("name");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("state_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.City, "city");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.HasIndex(e => e.Nit, "nit")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "phone")
                    .IsUnique();

                entity.HasIndex(e => e.State, "state");

                entity.HasIndex(e => e.Token, "token")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Email)
                    .HasMaxLength(160)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(40)
                    .HasColumnName("firstname");

                entity.Property(e => e.Language)
                    .HasMaxLength(2)
                    .HasColumnName("language")
                    .HasDefaultValueSql("'es'");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(40)
                    .HasColumnName("lastname");

                entity.Property(e => e.Nit).HasColumnName("nit");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .HasColumnName("phone");

                entity.Property(e => e.Role)
                    .HasColumnType("enum('ADMIN','SUPER ADMIN','GENERAL')")
                    .HasColumnName("role")
                    .HasDefaultValueSql("'GENERAL'");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Token)
                    .HasMaxLength(20)
                    .HasColumnName("token");

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.City)
                    .HasConstraintName("user_ibfk_1");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("user_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
