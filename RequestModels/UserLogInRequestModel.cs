namespace ArtGallery.RequestModels
{
    public record UserLogInRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
