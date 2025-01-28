using ArtGallery.Domains;
using ArtGallery.Enums;
using ArtGallery.ExceptionModels;
using ArtGallery.Extensions;
using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;
using ArtGallery.ServiceInterfaces;
using ArtGallery.Validators;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ArtGallery.Services
{
    public class ProductService(IProductRepository productRepository, IMapper _mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IProductService
    {
        private long userId = httpContextAccessor.HttpContext.User.GetUserIdFromClaim();
        public async Task AddUpdateProductAsync(AddUpdateProductRequestModel model)
        {
            var validateRequest = await new ProductValidator().ValidateAsync(model);
            if (!validateRequest.IsValid)
                throw new ValidationException(validateRequest.Errors.Select(x => x.ErrorMessage).ToArray());

            Product product = new();

            if (model.Id > 0)
            {
               var products = await productRepository.GetProductsAsync(model.Id,null);
                product = products?.FirstOrDefault() ?? throw new NotFoundException("Employee Detail Not Found", model.Id);
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.IsAvailable = true;
            product.StatusEnum = ApprovalStatusEnum.Pending;
            product.AddedByUser = userId;

            Guid documentGroupId = Guid.NewGuid();

            Cloudinary cloudinary = new Cloudinary(configuration["AccountSettings:CLOUDINARY_URL"]);
            cloudinary.Api.Secure = true;

            if (model.File != null)
            {
                using (var stream = model.File.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams 
                    {
                        File = new FileDescription(model.File.FileName, stream),
                        PublicId = $"Products/Seller_{model.Id}/{documentGroupId}.{model.File.ContentType}"
                    };

                    if(model.ImageUrl != null)
                    {
                        await cloudinary.DeleteResourcesAsync(model.ImageUrl);
                    }
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    product.ImageUrl = uploadResult.Url.ToString();
                }
            }

            if (model.Id > 0)
            {
                productRepository.UpdateProductAsync(product);
            }
            else
            {
                await productRepository.AddProductAsync(product);
            }
        }

        public async Task DeleteProductAsync(long id)
        {
            var product = await productRepository.GetProductsAsync(id,userId);
            productRepository.DeleteProduct(product.FirstOrDefault()!);
        }

        public async Task<List<ProductResponseModel>> GetProductsAsync(long? id)
        {
            var products = await productRepository.GetProductsAsync(id,userId);
            var response = _mapper.Map<List<ProductResponseModel>>(products);
            return response;
        }


        public async Task<List<ProductResponseModel>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAllProductsAsync();
            var response = _mapper.Map<List<ProductResponseModel>>(products);
            return response;
        }

        public async Task<bool> LikeUnlikeProductAsync(LikeRequestModel likeRequestModel)
        {
            var like = await productRepository.GetLike(likeRequestModel.ProductId, userId);

            if (like != null)
            {
                productRepository.UnlikeProduct(like);
                return false;
            }
            else
            {
                like = new Like
                {
                    DateTime =  DateTime.Now,
                    LikedByUser = userId,
                    ProductId = likeRequestModel.ProductId,
                };
                await productRepository.LikeProductAsync(like);
                return true;
            }
        }

        public async Task<CommentResponseModel> AddUpdateCommentAsync(CommentRequestModel commentRequestModel)
        {
            Comment? comment = new();

            if(commentRequestModel.Id > 0)
            {
                comment = await productRepository.GetUserComment(commentRequestModel.Id, userId) ?? throw new NotFoundException("comment",commentRequestModel.Id);
            }

            comment.Id = commentRequestModel.Id;
            comment.ProductId = commentRequestModel.ProductId;
            if (commentRequestModel.Id == 0) comment.DateTime = DateTime.Now;
            comment.CommentText = commentRequestModel.CommentText;
            comment.CommentByUser = userId;

            if(commentRequestModel.Id > 0)
            {
                var commentUpdated = productRepository.UpdateCommentAsync(comment);
                var commentResponseModel = _mapper.Map<CommentResponseModel>(commentUpdated);
                return commentResponseModel;
            }
            else
            {
                var commentAdded = await productRepository.AddCommentAsync(comment);
                var commentResponseModel = _mapper.Map<CommentResponseModel>(commentAdded);
                return commentResponseModel;
            }

        }

        public async Task DeleteComment(long commentId)
        {
            var comment = await productRepository.GetUserComment(commentId,userId);
            productRepository.DeleteComment(comment);

        }

        public async Task<List<CommentResponseModel>> GetAllCommentsAsync(long productId)
        {
           var comments = await productRepository.GetAllComments(productId);
           var commentResponseModels = _mapper.Map<List<CommentResponseModel>>(comments);
            return commentResponseModels;
        }

        public async Task AddToCartAsync(long productId)
        {
            CartItem cartItem = new();
            cartItem.ProductId = productId;
            cartItem.UserId = userId;
            cartItem.datetime = DateTime.Now;
            await productRepository.AddToCartAsync(cartItem);
        }

        public async Task AddOrderAsync(List<long> productIds)
        {
            List<Order> orders = new();

            productIds.ForEach(productId =>
            {
                Order order = new();
                order.ProductId = productId;
                order.OrderBy = userId;
                order.DateTime = DateTime.Now;
                order.Status = OrderStatusEnum.Pending;
                orders.Add(order);  
            }); 
   
            await productRepository.AddOrderAsync(orders);
        }

        public async Task<List<OrderResponseModel>> GetOrdersAsync()
        {
            var orders = await productRepository.GetOrdersAsync(userId, null);
            var orderResponseModels = _mapper.Map<List<OrderResponseModel>>(orders);
            return orderResponseModels;
        }

        public async Task RemoveFromCartAsync(long cartId)
        {
            List<CartItem> carts = await productRepository.GetCartItemsAsync(cartId,userId);
            CartItem cart = carts.FirstOrDefault() ?? throw new NotFoundException("cart item", cartId);
            productRepository.DeleteCartItem(cart);
        }

        public async Task<List<CartResponseModel>> GetCartItemsAsync()
        {
            var cartItems = await productRepository.GetCartItemsAsync(null,userId);
            var cartResponseModels = _mapper.Map<List<CartResponseModel>>(cartItems);
            return cartResponseModels;
        }
    }
}
