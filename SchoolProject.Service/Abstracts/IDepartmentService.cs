using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<bool> IsExist(int id);

        Task<Department> GetById(int id);
    }
}