using ArtGallery.Domains;

namespace ArtGallery.ServiceInterfaces
{
    public interface IChatRepository
    {
        public Task AddMessageAsync(Message message);
        public void UpdateMessageAsync(Message message);
        public Task<Message?> GetMessageAsync(long messageId);
        public Task<List<Message>> GetAllMessageAsync(long friendUserId, long userId);

    }
}
