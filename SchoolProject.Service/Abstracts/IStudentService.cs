using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync();

        Task<Student> GetStudentByIdAsync(int id);

        Task<Student> AddAsync(Student student);

        Task<Student> UpdateAsync(Student student);

        Task<bool> DeleteAsync(Student student);

        Task<bool> IsExist(int id);
    }
}