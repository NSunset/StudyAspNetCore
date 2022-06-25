using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Nw.Account.MicroService.Repository
{
    public partial class AccountDbContext : DbContext
    {
        public AccountDbContext()
        {
        }

        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Account> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Models.Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id)
                    .HasColumnType("int(7)")
                    .HasColumnName("id");

                entity.Property(e => e.Residue)
                    .HasPrecision(10, 2)
                    .HasColumnName("residue")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("剩余可用额度");

                entity.Property(e => e.Total)
                    .HasPrecision(10, 2)
                    .HasColumnName("total")
                    .HasComment("总额度");

                entity.Property(e => e.Used)
                    .HasPrecision(10, 2)
                    .HasColumnName("used")
                    .HasComment("已用余额");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(7)")
                    .HasColumnName("user_id")
                    .HasComment("用户id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
