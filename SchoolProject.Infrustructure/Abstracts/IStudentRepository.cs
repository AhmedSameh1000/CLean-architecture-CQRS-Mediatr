using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}