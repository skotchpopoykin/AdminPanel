using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models;

/*
 * public partial class _1Context : DbContext: Класс _1Context наследуется от DbContext, 
 * который предоставляет основные функции для работы с базой данных. Класс помечен как partial,
 * что означает, что он может быть разделен на несколько файлов, что упрощает управление большим количеством кода.
 */
public partial class _1Context : DbContext   
{
    public _1Context()
    {
    }

    public _1Context(DbContextOptions<_1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }   //соответствие таблицы и модели

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=1;Username=postgres;Password=1qasw23ed");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Name, "uk_3g1j96g94xpk3lpxl2qbl985x").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Roles)
                .HasMaxLength(255)
                .HasColumnName("roles");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_roles");

            entity.HasIndex(e => e.RoleId, "uk_5q4rc4fh1on6567qk69uesvyf").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithOne()
                .HasForeignKey<UserRole>(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkh8ciramu9cc9q3qcqiv4ue8a6");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkhfh9dx7w3ubf1co1vdev94g3f");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
