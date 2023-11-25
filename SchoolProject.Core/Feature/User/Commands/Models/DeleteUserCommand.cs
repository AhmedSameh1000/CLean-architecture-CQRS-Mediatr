using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.User.Commands.Models
{
    public class DeleteuserCommand : IRequest<Response<string>>
    {
        public DeleteuserCommand(int Id)
        {
            this.Id = Id;
        }

        public int Id { get; }
    }
}