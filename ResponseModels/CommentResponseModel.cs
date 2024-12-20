namespace ArtGallery.ResponseModels
{
    public class CommentResponseModel
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public string CommentText { get; set; }
        public UserResponseModel User { get; set; }
    }
}
