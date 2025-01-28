using ArtGallery.Domains;
using ArtGallery.Enums;

namespace ArtGallery.ResponseModels
{
    public class OrderResponseModel
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public long OrderBy { get; set; }
        public OrderStatusEnum Status { get; set; }
        public ProductResponseModel? Product { get; set; }
    }
}
