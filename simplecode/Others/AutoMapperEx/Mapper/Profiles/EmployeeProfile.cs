using AutoMapper;
using AutoMapperEx.Mapper.DTO;
using AutoMapperEx.Mapper.Model;
using AutoMapperEx.Mapper.Shared;

namespace AutoMapperEx.Mapper.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, EmployeeModel>()
                .ForMember(empmodel => empmodel.FullName, empdto => empdto.MapFrom(empdto => empdto.Name))
                .ForMember(empmodel => empmodel.Salary, empdto => empdto.MapFrom(empdto => empdto.Age > 55 ? empdto.Salary * 10 : empdto.Salary))
                .ReverseMap();
            CreateMap<TelephoneNumberDTO, TelephoneNumberModel>()
                .ReverseMap();

            CreateMap<EmployeeDTO, EmployeeModel>()
                .ForMember(empmodel => empmodel.Telephone, empdto => empdto.NullSubstitute("Not Available"));
        }
    }
}
