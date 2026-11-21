namespace BookStore.API.Models;

// Composite key (FlashSaleId + BookId) will be configured in Fluent API
public class FlashSaleItem
{
    public int FlashSaleId { get; set; }
    public FlashSale FlashSale { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public decimal SalePrice { get; set; }
    public int QuantityLeft { get; set; }
}
