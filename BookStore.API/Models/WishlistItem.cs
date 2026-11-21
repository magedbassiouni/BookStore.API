namespace BookStore.API.Models;

// Composite key (UserId + BookId) will be configured in Fluent API
public class WishlistItem
{
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
