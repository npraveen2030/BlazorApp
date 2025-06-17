using Microsoft.EntityFrameworkCore;
using BlazorApp.Model.Entities.AuthDB.Core;
using BlazorApp.Model.Entities.AuthDB.Pricing;
using BlazorApp.Model.Entities.AuthDB.Personalization;

namespace BlazorApp.DLL.DBContext;

public partial class AuthDbContext : DbContext
{
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminRoleAssociation> AdminRoleAssociations { get; set; }

    public virtual DbSet<BonusType> BonusTypes { get; set; }

    public virtual DbSet<Evaluate> Evaluates { get; set; }

    public virtual DbSet<ExceptionInterest> ExceptionInterests { get; set; }

    public virtual DbSet<Floating> Floatings { get; set; }

    public virtual DbSet<MasterTabAssocaition> MasterTabAssocaitions { get; set; }

    public virtual DbSet<PricingModel> Models { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Tab> Tabs { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserGridAssoc> UserGridAssocs { get; set; }

    public virtual DbSet<UserProjectRoleAssociation> UserProjectRoleAssociations { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserTabAssoc> UserTabAssocs { get; set; }

    public virtual DbSet<preferencesMasterTable> preferencesMasterTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder.UseSqlServer("Server=LAPTOP-7FF2JANM;Database=AuthDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminRoleAssociation>(entity =>
        {
            entity.ToTable("AdminRoleAssociation", "Core");

            entity.HasOne(d => d.Role).WithMany(p => p.AdminRoleAssociations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminRoleAssociation_UserRoles");

            entity.HasOne(d => d.User).WithMany(p => p.AdminRoleAssociations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminRoleAssociation_UserDetails");
        });

        modelBuilder.Entity<BonusType>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__BonusTyp__3214EC27C8112D90");

            entity.ToTable("BonusType", "Pricing");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Evaluate>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Evaluate__3214EC27D0121318");

            entity.ToTable("Evaluate", "Pricing");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ExceptionInterest>(entity =>
        {
            entity.ToTable("ExceptionInterest", "Pricing");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AnnualizedCurrentInteresetExpense).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AnnualizedInterestExpense).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AnnualizedStandardIneterestExpense).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AppliedInterestExpires).HasColumnType("datetime");
            entity.Property(e => e.AppliedInterestRate).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AverageCollectedBalance).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CurrentInteresetRate).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.FloatingRate).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.StandardInterestRate).HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.Model).WithMany(p => p.ExceptionInterests)
                .HasForeignKey(d => d.Modelid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExceptionInterest_Models");

            entity.HasOne(d => d.Product).WithMany(p => p.ExceptionInterests)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExceptionInterest_Product");
        });

        modelBuilder.Entity<Floating>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Floating__3214EC27CAC6CDAE");

            entity.ToTable("Floating", "Pricing");

            entity.HasIndex(e => e.Name, "UQ__Floating__737584F628532D82").IsUnique();

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MasterTabAssocaition>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MasterTabAssocaition", "Personalization");

            entity.Property(e => e.TabName)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<PricingModel>(entity =>
        {
            entity.HasKey(e => e.Modelid).HasName("PK__Models__E8D4A544A0BEC966");

            entity.ToTable("Models", "Pricing");

            entity.Property(e => e.Modelid).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Product__3214EC27AABDA778");

            entity.ToTable("Product", "Pricing");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.AnnualizedCurrentInterestExpense)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.AnnualizedInterestExpense).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AnnualizedStandardInterestExpense).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AppliedInterestRate).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentInterestRate).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StandardInteresetRate).HasColumnType("decimal(18, 4)");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Tiles__7FCB7AE063243E74");

            entity.ToTable("Projects", "Core");

            entity.Property(e => e.ProjectName).HasMaxLength(100);
        });

        modelBuilder.Entity<Tab>(entity =>
        {
            entity.HasKey(e => e.TabId).HasName("PK__Tabs__80E37C1812CB7725");

            entity.ToTable("Tabs", "Pricing");

            entity.Property(e => e.TabId).ValueGeneratedNever();
            entity.Property(e => e.TabName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserDeta__1788CC4C7AE0F18A");

            entity.ToTable("UserDetails", "Core");

            entity.HasIndex(e => e.UserName, "IX_UserDetails").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserGridAssoc>(entity =>
        {
            entity.HasKey(e => e.UGAId);

            entity.ToTable("UserGridAssoc", "Personalization");
        });

        modelBuilder.Entity<UserProjectRoleAssociation>(entity =>
        {
            entity.HasKey(e => e.upraId);

            entity.ToTable("UserProjectRoleAssociation", "Core");

            entity.HasOne(d => d.Project).WithMany(p => p.UserProjectRoleAssociations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProjectRoleAssociation_Projects");

            entity.HasOne(d => d.Role).WithMany(p => p.UserProjectRoleAssociations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProjectRoleAssociation_UserRoles");

            entity.HasOne(d => d.User).WithMany(p => p.UserProjectRoleAssociations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProjectRoleAssociation_UserDetails");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE1A1860925D");

            entity.ToTable("UserRoles", "Core");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserTabAssoc>(entity =>
        {
            entity.HasKey(e => e.UTAId);

            entity.ToTable("UserTabAssoc", "Personalization");
        });

        modelBuilder.Entity<preferencesMasterTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("preferencesMasterTable", "Personalization");

            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
