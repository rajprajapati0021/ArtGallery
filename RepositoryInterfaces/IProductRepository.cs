using ArtGallery.Domains;
using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;

namespace ArtGallery.ServiceInterfaces
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public void UpdateProductAsync(Product product);
        public Task<List<Product>> GetProductsAsync(long? id,long? userId);
        public Task<List<Product>> GetAllProductsAsync();
        public void DeleteProduct(Product product);
        public Task LikeProductAsync(Like like);
        public void UnlikeProduct(Like like);
        public Task<Like?> GetLike(long productId, long userId);

        public Task<Comment> AddCommentAsync(Comment comment);
        public Comment UpdateCommentAsync(Comment comment);
        public Task<Comment?> GetUserComment(long commentId, long userId);
        public Task<List<Comment>> GetAllComments(long productId);
        public void DeleteComment(Comment comment);

    }
}
