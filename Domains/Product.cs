using ArtGallery.Enums;

namespace ArtGallery.Domains
{
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
        public User AddedByUser { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public ICollection<Comment>? Comments { get; set; }

    }
}