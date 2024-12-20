namespace ArtGallery.RequestModels
{
    public record LikeRequestModel
    {
        public DateTime DateTime { get; set; }
        public long ProductId { get; set; }
    }
}
