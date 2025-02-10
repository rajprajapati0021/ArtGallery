using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;

namespace ArtGallery.ServiceInterfaces
{
    public interface IChatService
    {
        public Task<MessageResponseModel> AddUpdateMessageAsync(AddUpdateMessageRequestModel model);

        public Task<List<MessageResponseModel>> GetAllMessageAsync(long friendUserId);

        public Task DeleteAllCloudinaryImages();

    }
}
