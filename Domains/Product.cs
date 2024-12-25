using ArtGallery.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.Domains
{
    [Table("products")]
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }    
        public int Stock  { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public ApprovalStatusEnum StatusEnum { get; set; }
        public long AddedByUser { get; set; }
        [ForeignKey(nameof(AddedByUser))] public User? User { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Comment>? Comments { get; set; }

    }
}