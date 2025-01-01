using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Domains;

[Table("cart_items")]
public class CartItem
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public DateTime datetime {  get; set; }
    [ForeignKey(nameof(ProductId))] public Product? Product { get; set; }
    [ForeignKey(nameof(UserId))]  public User? User { get; set; }

}
