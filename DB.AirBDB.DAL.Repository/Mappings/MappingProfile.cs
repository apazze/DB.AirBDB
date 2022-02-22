using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;

namespace DB.AirBDB.DAL.Repository.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Place, LugarDTO>()
                .ForMember(dest => dest.LugarId, map => map.MapFrom(src => src.PlaceId))
                .ForMember(dest => dest.Descricao, map => map.MapFrom(src => src.Description))
                .ForMember(dest => dest.TipoDeAcomodacao, map => map.MapFrom(src => src.AccomodationType))
                .ForMember(dest => dest.Cidade, map => map.MapFrom(src => src.City))
                .ForMember(dest => dest.Valor, map => map.MapFrom(src => src.Value))
                .ForMember(dest => dest.EstadoDaLocacao, map => map.MapFrom(src => src.StatusLocacao))
                .ForMember(dest => dest.ListaReservas, map => map.MapFrom(src => src.ListaReservas))
                .ReverseMap();

            CreateMap<Booking, ReservaDTO>()
                .ForMember(dest => dest.ReservaId, map => map.MapFrom(src => src.BookingId))
                .ForMember(dest => dest.LugarId, map => map.MapFrom(src => src.PlaceId))
                .ReverseMap();

            CreateMap<User, UsuarioDTO>()
                .ReverseMap();


        }
    }
}
