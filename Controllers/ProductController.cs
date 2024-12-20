using ArtGallery.RequestModels;
using ArtGallery.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    [Authorize]
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromForm] AddUpdateProductRequestModel productRequestModel)
    {
        await productService.AddUpdateProductAsync(productRequestModel);
        return Ok();
    }

    [Authorize]
    [HttpGet("get-product")]
    public async Task<IActionResult> GetProducts(long? productId)
    {
        var products = await productService.GetProductsAsync(productId);
        return Ok(products);
    }

    [Authorize]
    [HttpGet("get-all-product")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products);
    }

    [Authorize]
    [HttpPost("delete-product")]
    public async Task<IActionResult> DeleteProduct([FromBody]long productId)
    {
        await productService.DeleteProductAsync(productId);
        return Ok();
    }

    [Authorize]
    [HttpPost("like-unlike-product")]
    public async Task<IActionResult> LikeUnlikeProduct(LikeRequestModel likeRequestModel)
    {
        bool likeFlag = await productService.LikeUnlikeProductAsync(likeRequestModel);
        return Ok(likeFlag);
    }

    [Authorize]
    [HttpPost("add-comment")]
    public async Task<IActionResult> AddComment(CommentRequestModel commentRequestModel)
    {
        var comment = await productService.AddUpdateCommentAsync(commentRequestModel);
        return Ok(comment);
    }

    [Authorize]
    [HttpGet("get-comments")]
    public async Task<IActionResult> GetComments(long productId)
    {
        var products = await productService.GetAllCommentsAsync(productId);
        return Ok(products);
    }

    [Authorize]
    [HttpPost("delete-comment")]
    public async Task<IActionResult> DeleteComment(long commentId)
    {
        await productService.DeleteComment(commentId);
        return Ok();
    }

} 
