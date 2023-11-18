using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Models;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Queries.Handler
{
    public class StudentQueryHandler : ResponseHandler,
        IRequestHandler<GetStudentListQuery, Response<List<StudentToReturn>>>,
        IRequestHandler<GetStudentByIdQuery, Response<StudentToReturn>>
    {
        #region Fields

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Constractor

        public StudentQueryHandler(
            IStudentService studentService,
            IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        #endregion Constractor

        #region HandleFunction

        public async Task<Response<List<StudentToReturn>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList = await _studentService.GetStudentsAsync();

            var StudentListResponse = _mapper.Map<List<StudentToReturn>>(StudentList);
            return Success(StudentListResponse);
        }

        public async Task<Response<StudentToReturn>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var Student = await _studentService.GetStudentByIdAsync(request.Id);

            if (Student is null)
            {
                return NotFound<StudentToReturn>("Student Not Found");
            }

            var StudentResponse = _mapper.Map<StudentToReturn>(Student);

            return Success(StudentResponse);
        }

        #endregion HandleFunction
    }
}