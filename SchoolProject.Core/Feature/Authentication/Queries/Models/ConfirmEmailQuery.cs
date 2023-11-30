using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.DTOs;

namespace SchoolProject.Core.Feature.Authentication.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public ConfirmEmailDTO ConfirmEmailDTO { get; set; }
    }
}