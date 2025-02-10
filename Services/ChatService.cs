using ArtGallery.Domains;
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
    public class ChatService(IChatRepository chatRepository,IMapper _mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IChatService
    {
        private long userId = httpContextAccessor.HttpContext!.User.GetUserIdFromClaim();

        public async Task<MessageResponseModel> AddUpdateMessageAsync(AddUpdateMessageRequestModel model)
        {
            var validateRequest = await new MessageValidator().ValidateAsync(model);
            if (!validateRequest.IsValid)
                throw new ValidationException(validateRequest.Errors.Select(x => x.ErrorMessage).ToArray());

            Message message = new();

            if (model.Id > 0)
            {
                 message = await chatRepository.GetMessageAsync(model.Id) ?? throw new NotFoundException("Message Not Found", model.Id);
            }

            message.Text = model.Text;
            message.Type = model.Type;
            message.Time = model.Time;
            message.SenderId = model.SenderId;
            message.RecieverId = model.RecieverId;

            Cloudinary cloudinary = new Cloudinary(configuration["AccountSettings:CLOUDINARY_URL"]);
            cloudinary.Api.Secure = true;
            if (model.File != null)
            {
                using (var stream = model.File.OpenReadStream())
                {
                    Guid FileId = Guid.NewGuid();
                    var uploadParams = new RawUploadParams
                    {
                        File = new FileDescription(model.File.FileName, stream),
                        PublicId = $"ChatImages/{userId}to{model.RecieverId}/{FileId}/{model.File.FileName}.{model.File.ContentType}"
                    };

                    if (model.FileUrl != null)
                    {
                        await cloudinary.DeleteResourcesAsync(model.FileUrl);
                    }
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    message.FileName = model.File.FileName;
                    message.FileUrl = uploadResult.Url.ToString();
                }

            }

            message.UserId = userId;

            if (model.Id > 0)
            {
                chatRepository.UpdateMessageAsync(message);
            }
            else
            {
                await chatRepository.AddMessageAsync(message);
            }

            var res = _mapper.Map<MessageResponseModel>(message, opt =>
            {
                opt.Items["UserId"] = userId;
            });

            return res;

        }

        public async Task<List<MessageResponseModel>> GetAllMessageAsync(long friendUserId)
        {
            var messages = await chatRepository.GetAllMessageAsync(friendUserId, userId) ?? throw new NotFoundException("Message Not Found",friendUserId);

            return _mapper.Map<List<MessageResponseModel>>(messages, opt =>
            {
                opt.Items["UserId"] = userId;
            });
        }

        public async Task DeleteAllCloudinaryImages()
        {

            Cloudinary cloudinary = new Cloudinary(configuration["AccountSettings:CLOUDINARY_URL"]);
            cloudinary.Api.Secure = true;

        }
    }
}
