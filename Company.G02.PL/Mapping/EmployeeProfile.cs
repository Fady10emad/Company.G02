using AutoMapper;
using Company.G02.DAL.Models;
using Company.G02.PL.Dots;

namespace Company.G02.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
