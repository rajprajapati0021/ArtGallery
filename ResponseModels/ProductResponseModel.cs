using ArtGallery.Domains;
using ArtGallery.Enums;

namespace ArtGallery.ResponseModels
{
    public class ProductResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public ApprovalStatusEnum StatusEnum { get; set; }
        public UserResponseModel? User { get; set; }
        public List<Like>? Likes { get; set; }
        public List<Comment>? Comments { get; set; }

    }
}
