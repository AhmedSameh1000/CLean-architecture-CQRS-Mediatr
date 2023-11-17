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
            var Student = await _studentRepository.GetStudentsAsync();

            return Student;
        }

        #endregion HandleFunction
    }
}