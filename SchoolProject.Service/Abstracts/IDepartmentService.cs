namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<bool> IsExist(int id);
    }
}