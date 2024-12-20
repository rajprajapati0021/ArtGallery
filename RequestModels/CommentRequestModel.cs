namespace ArtGallery.RequestModels
{
    public record CommentRequestModel
    {
        public long  Id { get; set; }  
        public string CommentText { get; set; }
        public DateTime DateTime { get; set; }
        public long ProductId { get; set; }
    }
}
