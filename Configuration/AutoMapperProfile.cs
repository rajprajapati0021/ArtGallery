using ArtGallery.Domains;
using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;
using ArtGallery.Extensions;

using AutoMapper;
using System.Security.Claims;

namespace ArtGallery.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //AddUpdateEmployeeRequestModel
            CreateMap<UserResponseModel, User>().ReverseMap();
            CreateMap<UserRequestModel, User>().ReverseMap();
            CreateMap<ProductResponseModel, Product>().ReverseMap();
            CreateMap<Comment, CommentResponseModel>().ReverseMap();
            CreateMap<CartItem, CartResponseModel>().ReverseMap();
            CreateMap<Order,  OrderResponseModel>().ReverseMap();
            CreateMap<Message, MessageResponseModel>()
                .ForMember(d => d.Type,
                    opt => opt.MapFrom(
                        (src, dst, _, context) => (long)context.Items["UserId"] != src.SenderId ? "sender" : "repaly"
                    )
                );
            //CreateMap<Message, MessageResponseModel>();
        }
    }
}
