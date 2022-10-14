using AutoMapper;
using EmployeeManager.Domain.Core.Dtos;
using EmployeeManager.Domain.Core.Models;
#nullable disable

namespace EmployeeManager.Domain.Core.Mapper.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(eDto => eDto.RolesIds, opt => opt
                .MapFrom((e, eDto) => eDto.RolesIds = e.EmployeeRole.Select(er => er.RoleId)));

            CreateMap<NewEmployeeDto, Employee>()
                .ForMember(e => e.EmployeeRole, opt => opt
                .MapFrom((eDto, e) => e.EmployeeRole = eDto.RolesIds
                    .Select(id => new EmployeeRole() { RoleId = id })));

            CreateMap<EditEmployeeDto, Employee>()
                .ForMember(e => e.EmployeeRole, opt => opt
                .MapFrom((eDto, e) => e.EmployeeRole = eDto.RolesIds
                    .Select(id => new EmployeeRole() { RoleId = id, EmployeeId = eDto.Id })));
        }
    }
}
