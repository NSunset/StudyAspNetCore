using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Order.MicroService.Repository
{
    public partial class OrderDbContext : DbContext
    {
        public OrderDbContext()
        {
        }

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Models.Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id)
                    .HasColumnType("int(7)")
                    .HasColumnName("id");

                entity.Property(e => e.Count)
                    .HasColumnType("int(7)")
                    .HasColumnName("count")
                    .HasComment("数量");

                entity.Property(e => e.Money)
                    .HasPrecision(7, 2)
                    .HasColumnName("money")
                    .HasComment("金额");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(7)")
                    .HasColumnName("product_id")
                    .HasComment("产品id");

                entity.Property(e => e.Status)
                    .HasColumnType("int(1)")
                    .HasColumnName("status")
                    .HasComment("订单状态：0：创建中；1：已完结");

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
