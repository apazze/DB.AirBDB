using AutoMapper;
using DB.AirBDB.Common.Model.DTO;
using DB.AirBDB.DAL.Repository.DatabaseEntity;

namespace DB.AirBDB.DAL.Repository.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lugar, LugarDTO>()
                .ReverseMap();

            CreateMap<Reserva, ReservaDTO>()
                .ReverseMap();

            CreateMap<Usuario, UsuarioDTO>()
                .ReverseMap();
            
            CreateMap<UsuarioDTO, UsuarioPostDTO>()
                .ReverseMap();
            
            CreateMap<ReservaDTO, ReservaPostDTO>()
                .ReverseMap();
            
            CreateMap<LugarDTO, LugarPostDTO>()
                .ReverseMap();


        }
    }
}
