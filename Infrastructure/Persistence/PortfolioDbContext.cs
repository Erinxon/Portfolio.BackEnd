using System;
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

        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Proyect> Proyects { get; set; }
        public virtual DbSet<ProyectSkill> ProyectSkills { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ViewLanguage> ViewLanguages { get; set; }
        public virtual DbSet<ViewLevel> ViewLevels { get; set; }
        public virtual DbSet<ViewPlatform> ViewPlatforms { get; set; }
        public virtual DbSet<ViewProyect> ViewProyects { get; set; }
        public virtual DbSet<ViewProyectSkill> ViewProyectSkills { get; set; }
        public virtual DbSet<ViewWorkExperience> ViewWorkExperiences { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
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
                    .IsRequired()
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
                    .IsRequired()
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
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Proyect)
                    .WithMany(p => p.ProyectSkills)
                    .HasForeignKey(d => d.ProyectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectSk__Proye__1CBC4616");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.ProyectSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProyectSk__Skill__1DB06A4F");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

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
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewLanguage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Languages");

                entity.Property(e => e.LanguageId).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewLevel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Levels");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LevelId).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewPlatform>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Platforms");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlatformId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ViewProyect>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Proyects");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

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
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlatformName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewProyectSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_ProyectSkills");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LevelName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewWorkExperience>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_WorkExperience");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.WorkExperienceId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.ToTable("WorkExperience");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WorkExperiences)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__WorkExper__UserI__1332DBDC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
