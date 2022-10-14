using AutoMapper;
using EmployeeManager.Domain.Core.Mapper.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Business.UnitTests.Factories
{
    internal static class MapperFactory
    {
        public static Mapper GetMapper()
        {
            List<Profile> profiles = new List<Profile>();
            profiles.Add(new EmployeeProfile());
            profiles.Add(new DepartmentProfile());
            profiles.Add(new RoleProfile());

            var mapperCofig = new MapperConfiguration(c => c.AddProfiles(profiles));
            var mapper = new Mapper(mapperCofig);

            return mapper;
        }
    }
}
