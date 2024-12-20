using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ArtGallery.Domains;

public class Like
{
    public long Id { get; set; }
    public DateTime DateTime { get; set; }
    public long LikedByUser { get; set; }
    public long ProductId { get; set; }
    [JsonIgnore]
    [ForeignKey(nameof(LikedByUser))] public User? User { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(ProductId))] public Product? Product { get; set; }


}