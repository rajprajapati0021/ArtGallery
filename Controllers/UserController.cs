using ArtGallery.RequestModels;
using ArtGallery.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(UserRequestModel userRequestModel)
    {
        await userService.AddUpdateUserAsync(userRequestModel);
        return Ok();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(UserLogInRequestModel LogInRequestModel)
    {
        var token = await userService.SignInUserAsync(LogInRequestModel);
            return Ok(new { token = token});
    }

    [HttpGet]
    public async Task<IActionResult> UserDetail(long? userId,string? email)
    {
        var userDetails = await userService.GetUserAsync(userId,email);
        return Ok(userDetails);
    }

}   
