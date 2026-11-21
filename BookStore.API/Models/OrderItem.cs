namespace BookStore.API.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // price at the time of purchase

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}
