namespace ArtGallery.RequestModels
{
    public record AddUpdateProductRequestModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? File { get; set; } 
        public string? ImageUrl { get; set; }

    }
}
