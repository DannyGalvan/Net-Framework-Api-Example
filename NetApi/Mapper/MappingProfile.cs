using AutoMapper;
using FluentValidation.Results;
using NetApi.Models;
using NetApi.Models.Request;
using NetApi.Models.Response;

namespace NetApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Source, Destination>();

            //Mapper de Usuarios
            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm:ss") : null));

            //Mapper de Productos
            CreateMap<ProductRequest, Product>();

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm:ss") : null))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.UserCreator, opt => opt.MapFrom(src => src.UserCreator))
                .ForMember(dest => dest.UserUpdater, opt => opt.MapFrom(src => src.UserUpdater));

            //Mapper de Errores de Validación
            CreateMap<ValidationFailure, ValidationError>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.PropertyName))
                .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage))
                .ForMember(dest => dest.AttemptedValue, opt => opt.MapFrom(src => src.AttemptedValue));
        }
    }
}