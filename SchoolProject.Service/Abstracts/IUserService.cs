using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;
using X.PagedList;

namespace SchoolProject.Service.Abstracts
{
    public interface IUserService
    {
        Task<IPagedList<User>> GetUsersAsync(RequestParams requestParams);

        int GetCount();
    }
}