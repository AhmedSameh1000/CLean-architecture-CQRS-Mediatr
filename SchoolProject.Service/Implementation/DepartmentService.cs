using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> GetById(int id)
        {
            var Department = await _departmentRepository.GetFirstOrDefault(c => c.DID == id, new[] { "Students", "Instructors", "Subjects", "Instructor" });

            return Department;
        }

        public Task<bool> IsExist(int id)
        {
            return _departmentRepository.IsExist(id);
        }
    }
}