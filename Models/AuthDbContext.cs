using System;
using System.Collections.Generic;
using BlazorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Models;

public partial class AuthDbContext : DbContext
{
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserProjectAssociation> UserProjectAssociations { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserRoleAssociation> UserRoleAssociations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-7FF2JANM;Database=AuthDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Tiles__7FCB7AE063243E74");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserDeta__1788CC4C8C363493");
        });

        modelBuilder.Entity<UserProjectAssociation>(entity =>
        {
            entity.HasKey(e => e.UserProjectId).HasName("PK__UserProj__5F7DD49794C56259");

            entity.HasOne(d => d.Project).WithMany(p => p.UserProjectAssociations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProjects_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.UserProjectAssociations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProjects_UserDetails");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE1A1860925D");
        });

        modelBuilder.Entity<UserRoleAssociation>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A35D871E4A7");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoleAssociations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleAssociations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_UserDetails");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
