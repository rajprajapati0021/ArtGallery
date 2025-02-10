using ArtGallery.Domains;
using ArtGallery.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Repositories
{
    public class ChatRepository(ArtGallleryContext artGallleryContext) : IChatRepository
    {
        public async Task AddMessageAsync(Message message)
        {
            try
            {

            await artGallleryContext.Messages.AddAsync(message);
            await artGallleryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<Message>> GetAllMessageAsync(long friendUserId, long userId)
        {
            return await artGallleryContext.Messages
                   .Where(x => (x.SenderId == userId && x.RecieverId == friendUserId) || (x.SenderId == friendUserId && x.RecieverId == userId))
                   .ToListAsync();
        }

        public async Task<Message?> GetMessageAsync(long messageId)
        {
            return await artGallleryContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
        }

        public void UpdateMessageAsync(Message message)
        {
             artGallleryContext.Messages.Update(message);
             artGallleryContext.SaveChanges();
        }
    }
}