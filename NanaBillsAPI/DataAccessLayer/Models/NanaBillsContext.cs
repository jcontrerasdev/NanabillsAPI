using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Models
{
    public partial class NanaBillsContext : DbContext
    {
        public NanaBillsContext()
        {
        }

        public NanaBillsContext(DbContextOptions<NanaBillsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Expense> Expenses { get; set; } = null!;
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
        public virtual DbSet<Income> Incomes { get; set; } = null!;
        public virtual DbSet<IncomeCategory> IncomeCategories { get; set; } = null!;
        public virtual DbSet<Investment> Investments { get; set; } = null!;
        public virtual DbSet<InvestmentCategory> InvestmentCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=WalletSur;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("expense");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdCurrency).HasColumnName("id_currency");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Pay).HasColumnName("pay");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expense_expenseCategory");

                entity.HasOne(d => d.IdCurrencyNavigation)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.IdCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expense_currency");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expense_user");
            });

            modelBuilder.Entity<ExpenseCategory>(entity =>
            {
                entity.ToTable("expenseCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.IdUser).HasColumnName("id_user");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("income");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdCurrency).HasColumnName("id_currency");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Incomes)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_income_incomeCategory");

                entity.HasOne(d => d.IdCurrencyNavigation)
                    .WithMany(p => p.Incomes)
                    .HasForeignKey(d => d.IdCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_income_currency");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Incomes)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_income_user");
            });

            modelBuilder.Entity<IncomeCategory>(entity =>
            {
                entity.ToTable("incomeCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.ToTable("investment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(14, 4)")
                    .HasColumnName("amount");

                entity.Property(e => e.Closed).HasColumnName("closed");

                entity.Property(e => e.ClosedAmount)
                    .HasColumnType("decimal(14, 4)")
                    .HasColumnName("closed_amount");

                entity.Property(e => e.ClosedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("closed_date");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdCurrency).HasColumnName("id_currency");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_investment_investmentCategory");

                entity.HasOne(d => d.IdCurrencyNavigation)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.IdCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_investment_currency");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_investment_user");
            });

            modelBuilder.Entity<InvestmentCategory>(entity =>
            {
                entity.ToTable("investmentCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
