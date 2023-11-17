using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IUnitOfWork
    {
        IStudentRepository Student { get; }

        Task BeginTransaction();

        Task Commit();

        Task Rollback();

        Task<bool> SaveChanges();
    }
}