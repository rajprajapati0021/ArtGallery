using ArtGallery.Domains;
using ArtGallery.RequestModels;
using ArtGallery.ResponseModels;
using AutoMapper;

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



            //CreateMap<AddUpdateAddressRequestModel, Address>()
            //    .ForMember(dest => dest.City, act => act.MapFrom(src => src.CityName)).ReverseMap();

            //CreateMap<Employee, EmployeeResponseModel>()
            //    .ForMember(dest => dest.Phone, act => act.MapFrom(src => src.PhoneNumber));
            //CreateMap<Address, AddressResponseModel>()
            //    .ForMember(dest => dest.CityName, act => act.MapFrom(src => src.City));

        }
    }
}
