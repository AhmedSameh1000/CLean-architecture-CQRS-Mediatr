using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;

namespace SchoolProject.Infrustructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        #region Fiels

        private readonly AppDbContext _dbContext;

        #endregion Fiels

        #region Constructor

        public StudentRepository(AppDbContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }

        #endregion Constructor
    }
}