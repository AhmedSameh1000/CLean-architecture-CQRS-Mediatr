using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Core.Feature.Students.DTOs;
using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Core.Resources;
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
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #endregion Fildes

        #region Constractor

        public StudentCommandHandler(
            IStudentService studentService,
            IMapper mapper,
            IValidator<CreateStudentDTO> CreateStudentValidator,
            IValidator<UpdateStudentDTO> UpdateStudentValidator,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _createStudentValidator = CreateStudentValidator;
            _updateStudentValidator = UpdateStudentValidator;
            _stringLocalizer = stringLocalizer;
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
                return BadRequest<StudentToReturn>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
        }

        public async Task<Response<StudentToReturn>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var IsExist = await _studentService.GetStudentByIdAsync(request.UpdateStudentDTO.Id);

            if (IsExist is null)
            {
                return NotFound<StudentToReturn>(_stringLocalizer[SharedSesourcesKeys.NotFound]);
            }

            var result = await _updateStudentValidator.ValidateAsync(request.UpdateStudentDTO);

            if (!result.IsValid)
            {
                return BadRequest<StudentToReturn>(string.Join(",", result.Errors.Select(c => c.ErrorMessage)));
            }

            IsExist = _mapper.Map<Student>(request.UpdateStudentDTO);

            await _studentService.UpdateAsync(IsExist);

            if (IsExist != null)
            {
                var StudentToReturn = _mapper.Map<StudentToReturn>(IsExist);
                return Success(StudentToReturn);
            }
            else
            {
                return BadRequest<StudentToReturn>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
            }
        }

        public async Task<Response<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var Student = await _studentService.GetStudentByIdAsync(request.Id);

            if (Student is null)
            {
                return NotFound<bool>(_stringLocalizer[SharedSesourcesKeys.NotFound]);
            }

            var isDeleted = await _studentService.DeleteAsync(Student);

            if (isDeleted)
                return Deleted<bool>();
            else
                return BadRequest<bool>(_stringLocalizer[SharedSesourcesKeys.BadRequest]);
        }

        #endregion HandleFunctions
    }
}