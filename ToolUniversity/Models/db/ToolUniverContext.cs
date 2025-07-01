using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToolUniversity.Models.db;

public partial class ToolUniverContext : DbContext
{
    public ToolUniverContext()
    {
    }

    public ToolUniverContext(DbContextOptions<ToolUniverContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BrokenInfor> BrokenInfors { get; set; }

    public virtual DbSet<FeeInfor> FeeInfors { get; set; }

    public virtual DbSet<LendInfor> LendInfors { get; set; }

    public virtual DbSet<ManagerInfor> ManagerInfors { get; set; }

    public virtual DbSet<OfficerInfor> OfficerInfors { get; set; }

    public virtual DbSet<ReturnInfor> ReturnInfors { get; set; }

    public virtual DbSet<ToolInfor> ToolInfors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = DESKTOP-2CPE3OP;Database=ToolUniver;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BrokenInfor>(entity =>
        {
            entity.HasKey(e => e.BrokenId).HasName("PK__Broken_I__E71DE43E11355ED4");

            entity.ToTable("Broken_Infor");

            entity.Property(e => e.BrokenId).HasColumnName("Broken_ID");
            entity.Property(e => e.OfficerId).HasColumnName("OfficerID");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ToolId).HasColumnName("Tool_ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Officer).WithMany(p => p.BrokenInfors)
                .HasForeignKey(d => d.OfficerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Broken_In__Offic__5EBF139D");

            entity.HasOne(d => d.Tool).WithMany(p => p.BrokenInfors)
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Broken_In__Tool___5DCAEF64");

            entity.HasOne(d => d.User).WithMany(p => p.BrokenInfors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Broken_In__UserI__5CD6CB2B");
        });

        modelBuilder.Entity<FeeInfor>(entity =>
        {
            entity.HasKey(e => e.FeeId).HasName("PK__Fee_Info__10B8D20757DAF75B");

            entity.ToTable("Fee_Infor");

            entity.Property(e => e.FeeId).HasColumnName("Fee_ID");
            entity.Property(e => e.LendId).HasColumnName("Lend_ID");
            entity.Property(e => e.ReturnId).HasColumnName("Return_ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Lend).WithMany(p => p.FeeInfors)
                .HasForeignKey(d => d.LendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fee_Infor__Lend___59063A47");

            entity.HasOne(d => d.Return).WithMany(p => p.FeeInfors)
                .HasForeignKey(d => d.ReturnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fee_Infor__Retur__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.FeeInfors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fee_Infor__UserI__5812160E");
        });

        modelBuilder.Entity<LendInfor>(entity =>
        {
            entity.HasKey(e => e.LendId).HasName("PK__Lend_Inf__FCA2DDE2FDAB5D24");

            entity.ToTable("Lend_Infor");

            entity.Property(e => e.LendId).HasColumnName("Lend_ID");
            entity.Property(e => e.DateLend)
                .HasColumnType("datetime")
                .HasColumnName("Date_Lend");
            entity.Property(e => e.ToolId).HasColumnName("Tool_ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Tool).WithMany(p => p.LendInfors)
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lend_Info__Tool___4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.LendInfors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lend_Info__UserI__5070F446");
        });

        modelBuilder.Entity<ManagerInfor>(entity =>
        {
            entity.HasKey(e => e.ManagerId);

            entity.ToTable("Manager_Infor");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OfficerInfor>(entity =>
        {
            entity.HasKey(e => e.OfficerId);

            entity.ToTable("Officer_Infor");

            entity.Property(e => e.OfficerId).HasColumnName("OfficerID");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReturnInfor>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__Return_I__0F4F4C5666381D5C");

            entity.ToTable("Return_Infor");

            entity.Property(e => e.ReturnId).HasColumnName("Return_ID");
            entity.Property(e => e.DateReturn)
                .HasColumnType("datetime")
                .HasColumnName("Date_Return");
            entity.Property(e => e.ToolId).HasColumnName("Tool_ID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Tool).WithMany(p => p.ReturnInfors)
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Return_In__Tool___534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.ReturnInfors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Return_In__UserI__5441852A");
        });

        modelBuilder.Entity<ToolInfor>(entity =>
        {
            entity.HasKey(e => e.ToolId);

            entity.ToTable("Tool_Infor");

            entity.Property(e => e.ToolId).HasColumnName("Tool_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ToolName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tool_Name");
            entity.Property(e => e.ToolType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tool_Type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Faculty)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Major)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
