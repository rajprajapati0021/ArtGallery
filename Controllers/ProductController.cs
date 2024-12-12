using ArtGallery.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController( ) : ControllerBase
{
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct(AddUpdateProductRequestModel productRequestModel)
    {
        //await userService.AddUpdateUserAsync(userRequestModel);
        return Ok();
    }


    //[HttpGet("get-product")]
    //public  Task<IActionResult> (long? userId,string? email)
    //{
    //    //var userDetails = await userService.GetUserAsync(userId,email);
    //    return Ok(userDetails);
    //}

}   
