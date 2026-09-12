using Application.Interfaces.Base;
using Domain.DTO.EmployeeDataTransferObjects;
using MediatR;
using Shared.Common;

namespace Application.Features.EmployeeFeature.Query.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ResponseModel<List<EmployeeDto>>>
    {
        private readonly IRepositoryManager _repoManager;


        public GetAllEmployeesQueryHandler(IRepositoryManager repoManager)
        {
            _repoManager = repoManager;
        }

        public async Task<ResponseModel<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repoManager.employeeRepository.GetListAsync();
            if (employees == null)
            {
                return new ResponseModel<List<EmployeeDto>>
                {
                    Success = false,
                    Message = "No employees found.",
                    Data = null
                };
            }
            List<EmployeeDto> employeeDtos = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = e.FullName,
                EmailAddress = e.EmailAddress,
                IsDeleted = e.IsDeleted
            }).ToList();
            return new ResponseModel<List<EmployeeDto>>
            {
                Success = true,
                Message = "Employees retrieved successfully.",
                Data = employeeDtos
            };
        }
    }
}

