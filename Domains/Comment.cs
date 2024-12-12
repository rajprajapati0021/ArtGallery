using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Domains;

public class Comment
{
    public long Id { get; set; }
    public DateTime DateTime { get; set; }
    public string CommentText { get; set; }
    public long CommentByUser { get; set; }
    [ForeignKey(nameof(CommentByUser))] public User User { get; set; }
}