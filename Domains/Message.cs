using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ArtGallery.Domains;
[Table("messages")]
public class Message
{
    public long Id { get; set; }
    public string? Text { get; set; }
    public string Type { get; set; }
    public string? FileUrl { get; set; } 
    public string? FileName { get; set; }
    public string Time { get; set; }
    public long SenderId { get; set; }
    public long RecieverId { get; set; }
    public long UserId { get; set; }
}