using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using X.PagedList;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        Task<IPagedList<Student>> GetStudentsAsync(RequestParams requestParams);

        Task<Student> GetStudentByIdAsync(int id);

        Task<Student> AddAsync(Student student);

        Task<Student> UpdateAsync(Student student);

        Task<bool> DeleteAsync(Student student);

        Task<bool> IsExist(int id);

        int GetCount();
    }
}