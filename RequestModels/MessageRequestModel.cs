namespace ArtGallery.RequestModels
{
    public record AddUpdateMessageRequestModel
    {
        public long Id { get; set; }
        public string? Text { get; set; }
        public string Type { get; set; }
        public IFormFile? File {get; set;}
        public string? FileUrl { get; set;}
        public string Time { get; set; }
        public long SenderId { get; set; }
        public long RecieverId { get; set; }

    }
}
