using ArtGallery.Domains;
using ArtGallery.ServiceInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ArtGallery.Repositories
{
    public class ProductRepository(ArtGallleryContext artGallleryContext) : IProductRepository
    {
        public async Task AddProductAsync(Product product)
        {
            await artGallleryContext.Products.AddAsync(product);
            await artGallleryContext.SaveChangesAsync();
        }

        public void DeleteProduct(Product product)
        {
            artGallleryContext.Products.Remove(product);
            artGallleryContext.SaveChanges();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await artGallleryContext.Products
              .Include(x => x.User)
              .Include(x => x.Likes)
              .Include(x => x.Comments).ToListAsync();
            return response;
        }

        public async Task<List<Product>> GetProductsAsync(long? id, long? userId)
        {
            var response = await artGallleryContext.Products
                .Include(x => x.User)
                .Include(x => x.Likes)
                .Include(x => x.Comments)
                .Where(x => (id == null || x.Id == id) && (userId == null || userId == x.AddedByUser)).ToListAsync();
            return response;
        }


        public void UpdateProductAsync(Product product)
        {
            artGallleryContext.Products.Update(product);
            artGallleryContext.SaveChanges();
        }
        public async Task LikeProductAsync(Like like)
        {
            await artGallleryContext.Likes.AddAsync(like);
            await artGallleryContext.SaveChangesAsync();
        }

        public void UnlikeProduct(Like like)
        {
             artGallleryContext.Likes.Remove(like);
            artGallleryContext.SaveChanges();

        }

        public async Task<Like?> GetLike(long productId,long userId)
        {
            return await artGallleryContext.Likes.FirstOrDefaultAsync(x => x.ProductId == productId && x.LikedByUser == userId);
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            await artGallleryContext.Comments.AddAsync(comment);
            await artGallleryContext.SaveChangesAsync();
            return comment;
        }

        public Comment UpdateCommentAsync(Comment comment)
        {
            artGallleryContext.Comments.Update(comment);
            artGallleryContext.SaveChanges();

            return comment;
        }

        public async Task<Comment?> GetUserComment(long commentId, long userId)
        {

            var comment = await artGallleryContext.Comments
                          //.Include(x => x.User)
                          .FirstOrDefaultAsync(x => x.Id == commentId && x.CommentByUser == userId);

            return comment;
        }

        public async Task<List<Comment>> GetAllComments(long productId)
        {
            var comments = await artGallleryContext.Comments
                          .Include(x => x.User)
                          .Where(x => x.ProductId == productId).OrderBy(x => x.Id).ToListAsync();

            return comments;
        }

        public void DeleteComment(Comment comment)
        {
            try
            {
                artGallleryContext.Comments.Remove(comment);
                artGallleryContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task AddToCartAsync(CartItem cartItem)
        {
            await artGallleryContext.CartItems.AddAsync(cartItem);
            await artGallleryContext.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsAsync(long? cartItemId, long userId)
        {
            var cartItems = await artGallleryContext.CartItems
                .Include(x => x.Product).ThenInclude(x => x.User)
                .Include(x => x.User)
                .Where(x => (cartItemId == null || x.Id == cartItemId) && x.UserId == userId)
                .ToListAsync();

            return cartItems;
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            artGallleryContext.CartItems.Remove(cartItem);
            artGallleryContext.SaveChanges();
        }
    }
}