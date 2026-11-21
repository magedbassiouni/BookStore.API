namespace BookStore.API.Models;

public class FlashSale
{
    public int Id { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }

    public ICollection<FlashSaleItem> Items { get; set; } = new List<FlashSaleItem>();
}
