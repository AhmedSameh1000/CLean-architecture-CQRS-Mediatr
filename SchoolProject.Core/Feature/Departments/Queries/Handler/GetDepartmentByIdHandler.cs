using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Departments.Queries.Models;
using SchoolProject.Core.Feature.Departments.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Departments.Queries.Handler
{
    public class GetDepartmentByIdHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public GetDepartmentByIdHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IDepartmentService departmentService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var Department = await _departmentService.GetById(request.Id);

            if (Department is null)
            {
                return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[SharedSesourcesKeys.NotFound]);
            }
            var DepartmentResponse = _mapper.Map<GetDepartmentByIdResponse>(Department);

            return Success(DepartmentResponse);
        }
    }
}