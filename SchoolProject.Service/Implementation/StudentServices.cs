using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class StudentServices : IStudentService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Fields

        #region Constructor

        public StudentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region HandleFunction

        public async Task<List<Student>> GetStudentsAsync()
        {
            var Student = await _unitOfWork.Student.GetAllAsNoTracking(new[] { "Department" });

            return Student.ToList();
        }

        #endregion HandleFunction
    }
}