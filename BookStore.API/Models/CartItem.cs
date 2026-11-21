namespace BookStore.API.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; } = 1;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}
