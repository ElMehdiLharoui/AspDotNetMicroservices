using AutoMapper;
using Domain.Entities;

namespace Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentDTO, StudentModel>();
        }
    }
}
