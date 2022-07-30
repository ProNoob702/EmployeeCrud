using AutoMapper;
using EmployeeCrud.Domain;
using EmployeeCrud.Web.Models.DTO;

namespace EmployeeCrud.Web.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
