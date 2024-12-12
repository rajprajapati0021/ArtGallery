using ArtGallery.Domains;

namespace ArtGallery.ServiceInterfaces
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public void UpdateUserAsync(User user);
        public Task<List<User>> GetUserAsync(long? id,string? email);
    }
}
