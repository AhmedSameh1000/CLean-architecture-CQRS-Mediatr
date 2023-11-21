namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IUnitOfWork
    {
        IStudentRepository Student { get; }
        IDepartmentRepository Department { get; }

        Task BeginTransaction();

        Task Commit();

        Task Rollback();

        Task<bool> SaveChanges();
    }
}