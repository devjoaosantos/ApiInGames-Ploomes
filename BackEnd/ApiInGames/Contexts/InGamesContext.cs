﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiInGames.Domains
{
    public partial class InGamesContext : DbContext
    {
        public InGamesContext()
        {
        }

        public InGamesContext(DbContextOptions<InGamesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estudio> Estudio { get; set; }
        public virtual DbSet<Jogo> Jogo { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4F680M8\\SQLEXPRESS; Initial Catalog=BDInGames;integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudio>(entity =>
            {
                entity.HasKey(e => e.IdEstudio);

                entity.HasIndex(e => e.NomeEstudio)
                    .HasName("UQ__Estudio__112A5690E61C0177")
                    .IsUnique();

                entity.Property(e => e.NomeEstudio)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Jogo>(entity =>
            {
                entity.HasKey(e => e.IdJogo);

                entity.HasIndex(e => e.NomeJogo)
                    .HasName("UQ__Jogo__89AF93E4FECF14B0")
                    .IsUnique();

                entity.Property(e => e.DataDeLancamento).HasColumnType("date");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NomeJogo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdEstudioNavigation)
                    .WithMany(p => p.Jogo)
                    .HasForeignKey(d => d.IdEstudio)
                    .HasConstraintName("FK__Jogo__IdEstudio__3A81B327");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuarios__A9D105349EA07D61")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Usuarios__IdTipo__403A8C7D");
            });
        }
    }
}