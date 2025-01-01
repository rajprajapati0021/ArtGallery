namespace ArtGallery.RequestModels;

public record CartRequestModel
{
    public long Id { get; set; }
    public long ProductId { get; set; }

}
