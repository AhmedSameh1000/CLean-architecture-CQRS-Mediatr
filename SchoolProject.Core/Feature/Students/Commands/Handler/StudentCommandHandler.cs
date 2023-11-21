using AutoMapper;
using FluentValidation;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Handler
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<CreateStudentCommand, Response<StudentToReturn>>,
        IRequestHandler<UpdateStudentCommand, Response<StudentToReturn>>,
        IRequestHandler<DeleteStudentCommand, Response<bool>>
    {
        #region Fildes

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateStudentDTO> _createStudentValidator;
        private readonly IValidator<UpdateStudentDTO> _updateStudentValidator;

        #endregion Fildes

        #region Constractor

        public StudentCommandHandler(
            IStudentService studentService,
            IMapper mapper,
            IValidator<CreateStudentDTO> CreateStudentValidator,
            IValidator<UpdateStudentDTO> UpdateStudentValidator)
        {
            _studentService = studentService;
            _mapper = mapper;
            _createStudentValidator = CreateStudentValidator;
            _updateStudentValidator = UpdateStudentValidator;
        }

        #endregion Constractor

        #region HandleFunctions

        public async Task<Response<StudentToReturn>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _createStudentValidator.ValidateAsync(request.CreateStudentDTO);

            if (!result.IsValid)
            {
                return BadRequest<StudentToReturn>(string.Join(",", result.Errors.Select(c => c.ErrorMessage)));
            }

            var Student = await _studentService.AddAsync(_mapper.Map<Student>(request.CreateStudentDTO));

            if (Student != null)
            {
                var StudentToReturn = _mapper.Map<StudentToReturn>(Student);
                return Success(StudentToReturn);
            }
            else
            {
                return BadRequest<StudentToReturn>("Error Whene Save Student");
            }
        }

        public async Task<Response<StudentToReturn>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _studentService.GetStudentByIdAsync(request.UpdateStudentDTO.Id);

            if (IsExist is null)
            {
                return NotFound<StudentToReturn>($"User With id {request.UpdateStudentDTO.Id} Not Found");
            }

            var result = await _updateStudentValidator.ValidateAsync(request.UpdateStudentDTO);

            if (!result.IsValid)
            {
                return BadRequest<StudentToReturn>(string.Join(",", result.Errors.Select(c => c.ErrorMessage)));
            }
            var UserToUpdate = _mapper.Map<Student>(request.UpdateStudentDTO);

            var Student = await _studentService.UpdateAsync(UserToUpdate);

            if (Student != null)
            {
                var StudentToReturn = _mapper.Map<StudentToReturn>(Student);
                return Success(StudentToReturn);
            }
            else
            {
                return BadRequest<StudentToReturn>("Error Whene Update Student");
            }
        }

        public async Task<Response<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var Student = await _studentService.GetStudentByIdAsync(request.Id);

            if (Student is null)
            {
                return NotFound<bool>($"Student With Id {request.Id} is not Fount");
            }

            var isDeleted = await _studentService.DeleteAsync(Student);

            if (isDeleted)
                return Deleted<bool>();
            else
                return BadRequest<bool>("Error While Delete this User");
        }

        #endregion HandleFunctions
    }
}