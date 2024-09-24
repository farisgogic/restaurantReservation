using AutoMapper;
using Restaurant_Model;
using Restaurant_Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Database.Users, Restaurant_Model.Users>()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

            CreateMap<UserInsertRequest, Database.Users>()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => new List<UserRole> {
                new UserRole { RoleId = 2 } // Assigning roleId 2 to a new UserRole
                }));

            CreateMap<UserUpdateRequest, Database.Users>();

            // Map UserRole from Model to Database
            CreateMap<Restaurant_Model.UserRole, Database.UserRole>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));

            // Map UserRole from Database to Model
            CreateMap<Database.UserRole, Restaurant_Model.UserRole>()
                .ForMember(dest => dest.UserRoleId, opt => opt.Ignore()) // Assuming UserRoleId is not set during mapping
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<Database.Role, Restaurant_Model.Roles>();
            CreateMap<RolesUpsertRequest, Database.Role>();
            CreateMap<Database.Table, Restaurant_Model.Table>();
            CreateMap<TableUpsertRequest, Database.Table>();
            CreateMap<Database.Reservation, Restaurant_Model.Reservation>();
            CreateMap<ReservationUpsertRequest, Database.Reservation>();
        }
    }

}
