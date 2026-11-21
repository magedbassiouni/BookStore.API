namespace BookStore.API.Models;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; } // 1 to 5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
