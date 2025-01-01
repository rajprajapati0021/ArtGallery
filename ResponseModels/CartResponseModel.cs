using ArtGallery.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGallery.ResponseModels;

public class CartResponseModel
{
    public long Id { get; set; }
    public DateTime datetime { get; set; }
    public ProductResponseModel? Product { get; set; }
    public UserResponseModel? User { get; set; }
}
