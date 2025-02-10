using ArtGallery.RequestModels;
using ArtGallery.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService chatService;

    public ChatController(IChatService chatService)
    {
        this.chatService = chatService;
    }

    [Authorize]
    [HttpPost("add-update-message")]
    public async Task<IActionResult> AddUpdateMessage([FromForm] AddUpdateMessageRequestModel messageRequestModel)
    {
        
        return Ok(await chatService.AddUpdateMessageAsync(messageRequestModel));
    }

    [Authorize]
    [HttpGet("get-all-message")]
    public async Task<IActionResult> GetAllMessage(long friendUserId)
    {
        var messages = await chatService.GetAllMessageAsync(friendUserId);
        return Ok(messages);
    }


    [HttpPost("delete-all-Images")]
    public async Task<IActionResult> DeleteAllImages()
    {
        await chatService.DeleteAllCloudinaryImages();
        return Ok();
    }

    ////[Authorize]
    //[HttpPost("delete-product")]
    //public async Task<IActionResult> DeleteProduct([FromBody] long productId)
    //{
    //    await productService.DeleteProductAsync(productId);
    //    return Ok();
    //}



}
