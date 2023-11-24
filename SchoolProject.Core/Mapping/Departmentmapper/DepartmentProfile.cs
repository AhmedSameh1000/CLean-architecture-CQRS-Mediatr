using AutoMapper;

namespace SchoolProject.Core.Mapping.Departmentmapper
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetDepartmentQueryMapping();
            GetDepartmentCommandMapping();
        }
    }
}