using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.User.Queries.Result;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Feature.User.Queries.Models
{
    public class GetusersListQuery : IRequest<Response<List<GetUserResponse>>>
    {
        public RequestParams RequestParams { get; set; }
    }
}