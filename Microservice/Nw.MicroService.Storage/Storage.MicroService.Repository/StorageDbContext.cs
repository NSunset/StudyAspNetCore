using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Storage.MicroService.Repository
{
    public partial class StorageDbContext : DbContext
    {
        public StorageDbContext()
        {
        }

        public StorageDbContext(DbContextOptions<StorageDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Storage> Storage { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=192.168.157.128;database=storagedb;uid=root;pwd=root;charset=utf8", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.37-mysql"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Models.Storage>(entity =>
            {
                entity.ToTable("storage");

                entity.Property(e => e.Id)
                    .HasColumnType("int(7)")
                    .HasColumnName("id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(7)")
                    .HasColumnName("product_id")
                    .HasComment("产品id");

                entity.Property(e => e.Residue)
                    .HasColumnType("int(10)")
                    .HasColumnName("residue")
                    .HasComment("剩余库存");

                entity.Property(e => e.Total)
                    .HasColumnType("int(10)")
                    .HasColumnName("total")
                    .HasComment("总库存");

                entity.Property(e => e.Used)
                    .HasColumnType("int(10)")
                    .HasColumnName("used")
                    .HasComment("已用库存");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
