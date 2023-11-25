using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.Queries.Result;

namespace SchoolProject.Core.Feature.User.Queries.Models
{
    public class GetuserByIdQuery : IRequest<Response<GetUserResponse>>
    {
        public GetuserByIdQuery(int Id)
        {
            this.Id = Id;
        }

        public int Id { get; }
    }
}