using AutoMapper;
using Entity.DAO;
using Entity.DTO;

namespace Entity.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EmployeeDAO, EmployeeDTO>().ReverseMap();
            CreateMap<DepartmentDAO, DepartmentDTO>().ReverseMap();
        }
    }
}
