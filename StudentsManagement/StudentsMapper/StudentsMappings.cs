using AutoMapper;
using StudentsManagement.Models;
using StudentsManagement.Models.Dtos;

namespace StudentsManagement.StudentsMapper
{
    public class StudentsMappings:Profile
    {
        public StudentsMappings()
        {
            CreateMap<Students, StudentsDto>().ReverseMap();
            CreateMap<Students, CreateStudentsDto>().ReverseMap();
            CreateMap<Students, UpdateStudentsDto>().ReverseMap();
            CreateMap<StudentsPersonalDetails, StudentsPersonalDetailsDto>().ReverseMap();
            CreateMap<StudentsPersonalDetails, CreateStudentsPersonalDetailsDto>().ReverseMap();
            CreateMap<StudentsPersonalDetails, UpdateStudentsPersonalDetailsDto>().ReverseMap();
            CreateMap<StudentsAddress, StudentsAddressDto>().ReverseMap();
            CreateMap<StudentsAddress, CreateStudentsAddressDto>().ReverseMap();
            CreateMap<StudentsAddress, UpdateStudentsAddressDto>().ReverseMap();

        }
    }
}
