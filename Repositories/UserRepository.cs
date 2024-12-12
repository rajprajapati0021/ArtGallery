using ArtGallery.Domains;
using ArtGallery.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories
{
    public class UserRepository(ArtGallleryContext artGallleryContext) : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
             await artGallleryContext.Users.AddAsync(user);
             await artGallleryContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUserAsync(long? id, string? email)
        {
            var response = await artGallleryContext.Users.Where(x => (id == null || x.Id == id) && (email == null || x.Email == email)).ToListAsync();
            return response;
        }

        public void  UpdateUserAsync(User user)
        {
             artGallleryContext.Users.Update(user);
             artGallleryContext.SaveChanges();  
        }
    }
}
