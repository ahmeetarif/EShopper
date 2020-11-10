using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShopper.DataAccess
{
    public partial class EShopperDbContext : IdentityDbContext<EShopperUser>
    {
        public EShopperDbContext(DbContextOptions<EShopperDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<SellerUsers> SellerUsers { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<UsersAddress> UsersAddress { get; set; }
        public virtual DbSet<UserDetails> UsersDetail { get; set; }
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.SellerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SellerUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.SellerUsers)
                    .HasForeignKey<SellerUsers>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Category_ParentId");
            });

            modelBuilder.Entity<UsersAddress>(entity =>
            {
                entity.Property(e => e.AddressLine1).IsRequired();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersAddress)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Token)
                    .IsRequired();

                entity.Property(e => e.JwtId)
                    .IsRequired();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.RefreshTokens)
                    .HasForeignKey<RefreshTokens>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserDetails)
                    .HasForeignKey<UserDetails>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);

            // Load Asp.Net Identity Default Table Configurations
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}