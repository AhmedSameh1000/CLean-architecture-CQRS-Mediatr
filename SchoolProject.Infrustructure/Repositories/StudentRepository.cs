using Microsoft.EntityFrameworkCore;
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

        #region HandelsFuction

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _dbContext.Students
                .AsNoTracking()
                .Include(c => c.Department)
                .ToListAsync();
        }

        #endregion HandelsFuction
    }
}