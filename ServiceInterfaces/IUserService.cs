using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;

namespace ArtGallery.ServiceInterfaces
{
    public interface IUserService
    {
        public Task AddUpdateUserAsync(UserRequestModel model);
        public Task<List<UserResponseModel>> GetUserAsync(long? id,string? email);
        public Task<string> SignInUserAsync(UserLogInRequestModel model);
    }
}
