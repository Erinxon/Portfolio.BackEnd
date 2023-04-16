﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public partial class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext()
        {
        }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<Platform> Platforms { get; set; } = null!;
        public virtual DbSet<Proyect> Proyects { get; set; } = null!;
        public virtual DbSet<ProyectSkill> ProyectSkills { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proyect>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DomainUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GithubUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.Proyects)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proyects__Platfo__6EF57B66");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Proyects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Proyects__UserId__6FE99F9F");
            });

            modelBuilder.Entity<ProyectSkill>(entity =>
            {
                entity.HasIndex(e => e.SkillId, "UQ__ProyectS__DFA09186F47A9058")
                    .IsUnique();

                entity.Property(e => e.ProyectSkillId).ValueGeneratedNever();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Proyect)
                    .WithMany(p => p.ProyectSkills)
                    .HasForeignKey(d => d.ProyectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectSk__Proye__74AE54BC");

                entity.HasOne(d => d.Skill)
                    .WithOne(p => p.ProyectSkill)
                    .HasForeignKey<ProyectSkill>(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectSk__Skill__75A278F5");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK__Skills__Language__693CA210");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK__Skills__LevelId__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Skills__UserId__6B24EA82");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.ToTable("WorkExperience");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PositionName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WorkExperiences)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__WorkExper__UserI__656C112C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}