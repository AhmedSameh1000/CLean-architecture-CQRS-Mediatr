using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Models;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Queries.Handler
{
    public class StudentHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>
    {
        #region Fields

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Constractor

        public StudentHandler(
            IStudentService studentService,
            IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        #endregion Constractor

        #region HandleFunction

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList = await _studentService.GetStudentsAsync();

            var StudentListResponse = _mapper.Map<List<GetStudentListResponse>>(StudentList);
            return Success(StudentListResponse);
        }

        #endregion HandleFunction
    }
}