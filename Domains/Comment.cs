using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ArtGallery.Domains;
[Table("comments")]
public class Comment
{
    public long Id { get; set; }
    public DateTime DateTime { get; set; }
    public string CommentText { get; set; }
    public long CommentByUser { get; set; }
    public long ProductId { get; set; }
    [JsonIgnore]
    [ForeignKey(nameof(CommentByUser))] public User User { get; set; }
    [JsonIgnore]
    [ForeignKey(nameof(ProductId))] public Product Product { get; set; }

}