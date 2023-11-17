using Microsoft.EntityFrameworkCore.Storage;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;

namespace SchoolProject.Infrustructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        private IDbContextTransaction _transaction;
        public IStudentRepository Student { get; private set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Student = new StudentRepository(dbContext);
        }

        public async Task BeginTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task Rollback()
        {
            await _transaction?.RollbackAsync();
            _transaction?.Dispose();
            _transaction = null;
        }

        public async Task<bool> SaveChanges()
        {
            var rowsEfected = await _dbContext.SaveChangesAsync();
            return rowsEfected > 0 ? true : false;
        }
    }
}