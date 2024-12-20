using ArtGallery.Domains;
using ArtGallery.ExceptionModels;
using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;
using ArtGallery.ServiceInterfaces;
using ArtGallery.Validators;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtGallery.Services
{
    public class UserService(IUserRepository userRepository,IMapper _mapper, IConfiguration configuration) : IUserService
    {
        public async Task AddUpdateUserAsync(UserRequestModel model)
        {
            var validateRequest = await new UserValidator().ValidateAsync(model);
            if (!validateRequest.IsValid)
                throw new ValidationException(validateRequest.Errors.Select(x => x.ErrorMessage).ToArray());

            var existingUser = await userRepository.GetUserAsync(null,model.Email);
            if (existingUser.Any() && model.Id == 0) throw new NotFoundException("Email already exist", model.Email);

            User user = new();

            if (model.Id > 0)
            {
                var users = await userRepository.GetUserAsync(model.Id, null);
                user = users?.FirstOrDefault() ?? throw new NotFoundException("Employee Detail Not Found", model.Id);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Age = model.Age;
            if ( model.Id == 0 ) user.Password = model.Password;
            user.Role = "user";

            if (user.Id > 0)
            {
                userRepository.UpdateUserAsync(user);
            }
            else
            {
                await userRepository.AddUserAsync(user);
            }
        }

        public async Task<List<UserResponseModel>> GetUserAsync(long? id, string? email)
        {
            var result = await userRepository.GetUserAsync(id, email);
            var response = _mapper.Map<List<UserResponseModel>>(result);
            return response;
        }

        public async Task<string> SignInUserAsync(UserLogInRequestModel model)
        {
            var result = await userRepository.GetUserAsync(null, model.Email);
            var user = result?.FirstOrDefault() ?? throw new NotFoundException("Invalid Email", model.Email);

            if(!(user.Email == model.Email && user.Password == model.Password)) throw new NotFoundException("Wrong Password", model.Email);
            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.NameIdentifier, $"{user.Id}"),
            new(ClaimTypes.Email, $"{user.Email}"),
            new(ClaimTypes.Role, $"{user.Role}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:SecurityKey").Value!));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration.GetSection("JWT:Issuer").Value,
                configuration.GetSection("JWT:Audience").Value,
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(360),
                signingCredentials: credential
            );

            //Created Refresh token
            var tokenHandler = new JwtSecurityTokenHandler();

            var accessToken = tokenHandler.WriteToken(token);

            return accessToken;
        }
    }
}
