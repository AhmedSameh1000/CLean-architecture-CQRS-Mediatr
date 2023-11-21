using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class StudentServices : IStudentService
    {
        #region Fields

        private readonly IStudentRepository _studentRepository;

        #endregion Fields

        #region Constructor

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        #endregion Constructor

        #region HandleFunction

        public async Task<List<Student>> GetStudentsAsync()
        {
            var Student = await _studentRepository.GetAllAsTracking(new[] { "Department" });

            return Student.ToList();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetFirstOrDefault(c => c.StudentId == id, new[] { "Department" });
        }

        public async Task<Student> AddAsync(Student student)
        {
            _studentRepository.ClearChangeTracking();
            await _studentRepository.Add(student);
            await _studentRepository.SaveChanges();

            return student;
        }

        public Task<bool> IsExist(int id)
        {
            return _studentRepository.IsExist(id);
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            _studentRepository.ClearChangeTracking();
            _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChanges();
            return student;
        }

        public async Task<bool> DeleteAsync(Student student)
        {
            _studentRepository.Remove(student);

            return await _studentRepository.SaveChanges();
        }

        #endregion HandleFunction
    }
}