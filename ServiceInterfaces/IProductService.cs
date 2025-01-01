using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;

namespace ArtGallery.ServiceInterfaces
{
    public interface IProductService
    {
        public Task AddUpdateProductAsync(AddUpdateProductRequestModel model);
        public Task<List<ProductResponseModel>> GetProductsAsync(long? id);
        public Task<List<ProductResponseModel>> GetAllProductsAsync();
        public Task<bool> LikeUnlikeProductAsync(LikeRequestModel likeRequestModel);
        public Task DeleteProductAsync(long id);

        public Task<CommentResponseModel> AddUpdateCommentAsync(CommentRequestModel commentRequestModel);
        public Task DeleteComment(long commentId);
        public Task<List<CommentResponseModel>> GetAllCommentsAsync(long productId);
        public Task AddToCartAsync(long productId);
        public Task RemoveFromCartAsync(long cartId);
        public Task<List<CartResponseModel>> GetCartItemsAsync();

    }
}
