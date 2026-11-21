using BookStore.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<FlashSale> FlashSales => Set<FlashSale>();
    public DbSet<FlashSaleItem> FlashSaleItems => Set<FlashSaleItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Required: configures the Identity tables (AspNetUsers, ...)
        base.OnModelCreating(builder);

        // ---------- ApplicationUser ----------
        builder.Entity<ApplicationUser>(e =>
        {
            e.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            e.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            e.Property(x => x.ProfileImage).HasMaxLength(500);
            e.Property(x => x.Address).HasMaxLength(300);
        });

        // ---------- Category / Publisher / Author ----------
        builder.Entity<Category>(e =>
        {
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
        });

        builder.Entity<Publisher>(e =>
        {
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
        });

        builder.Entity<Author>(e =>
        {
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        // ---------- Book ----------
        builder.Entity<Book>(e =>
        {
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Description).HasMaxLength(2000);
            e.Property(x => x.CoverImage).HasMaxLength(500);
            e.Property(x => x.ASIN).IsRequired().HasMaxLength(20);
            e.Property(x => x.Price).HasPrecision(18, 2);

            e.HasIndex(x => x.ASIN).IsUnique();

            // Restrict: you can't delete a Category/Publisher/Author that still has books
            e.HasOne(x => x.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(x => x.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- Review ----------
        builder.Entity<Review>(e =>
        {
            e.Property(x => x.Comment).HasMaxLength(1000);
        });

        // ---------- CartItem ----------
        builder.Entity<CartItem>(e =>
        {
            // The same book can't appear twice in one user's cart (increase Quantity instead)
            e.HasIndex(x => new { x.UserId, x.BookId }).IsUnique();
        });

        // ---------- WishlistItem (composite key) ----------
        builder.Entity<WishlistItem>(e =>
        {
            e.HasKey(x => new { x.UserId, x.BookId });
        });

        // ---------- Coupon ----------
        builder.Entity<Coupon>(e =>
        {
            e.Property(x => x.Code).IsRequired().HasMaxLength(50);
            e.Property(x => x.DiscountPercentage).HasPrecision(5, 2);
            e.HasIndex(x => x.Code).IsUnique();
        });

        // ---------- FlashSaleItem (composite key) ----------
        builder.Entity<FlashSaleItem>(e =>
        {
            e.HasKey(x => new { x.FlashSaleId, x.BookId });
            e.Property(x => x.SalePrice).HasPrecision(18, 2);
        });

        // ---------- Order ----------
        builder.Entity<Order>(e =>
        {
            e.Property(x => x.OrderNumber).IsRequired().HasMaxLength(20);
            e.Property(x => x.ShippingAddress).IsRequired().HasMaxLength(300);

            // Store the enum as text (InProgress, Completed, Canceled) instead of 1, 2, 3
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);

            e.Property(x => x.Subtotal).HasPrecision(18, 2);
            e.Property(x => x.ShippingCost).HasPrecision(18, 2);
            e.Property(x => x.Tax).HasPrecision(18, 2);
            e.Property(x => x.DiscountAmount).HasPrecision(18, 2);
            e.Property(x => x.Total).HasPrecision(18, 2);

            e.HasIndex(x => x.OrderNumber).IsUnique();

            // Keep order history even if something related gets deleted
            e.HasOne(x => x.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Coupon)
                .WithMany(c => c.Orders)
                .HasForeignKey(x => x.CouponId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- OrderItem ----------
        builder.Entity<OrderItem>(e =>
        {
            e.Property(x => x.UnitPrice).HasPrecision(18, 2);

            // Restrict: a book that was ordered can't be deleted
            e.HasOne(x => x.Book)
                .WithMany(b => b.OrderItems)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}