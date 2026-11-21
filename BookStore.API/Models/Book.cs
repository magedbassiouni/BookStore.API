namespace BookStore.API.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CoverImage { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PublicationYear { get; set; }
    public string ASIN { get; set; } = string.Empty;
    public int StockQuantity { get; set; }

    // Rate and ReviewCount are NOT stored: they are calculated from Reviews

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<FlashSaleItem> FlashSaleItems { get; set; } = new List<FlashSaleItem>();
}
