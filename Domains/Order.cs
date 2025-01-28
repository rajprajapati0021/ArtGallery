using ArtGallery.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Domains;

[Table("orders")]
public class Order
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public DateTime DateTime { get; set; }
    public long OrderBy { get; set; }
    public OrderStatusEnum Status {  get; set; }
    [ForeignKey(nameof(ProductId))] public Product Product { get; set; }    
}
