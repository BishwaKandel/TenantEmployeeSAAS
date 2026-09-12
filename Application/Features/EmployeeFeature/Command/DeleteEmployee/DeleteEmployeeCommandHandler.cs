using Application.Features.EmployeeFeature.Command.CreateEmployee;
using Application.Interfaces.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Features.EmployeeFeature.Command.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ResponseModel<string>>
    {
        private readonly IRepositoryManager _repo;

        public DeleteEmployeeCommandHandler(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repo.employeeRepository.FindByCondition(x => x.Id == request.Id, false).FirstOrDefaultAsync();
            if (employee == null)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Employee not found",
                    Data = null
                };

            }
            employee.IsDeleted = true;
            await _repo.employeeRepository.UpdateAsync(employee);

            return new ResponseModel<string>
            {
                Success = true,
                Message = "Employee deleted successfully",
                Data = employee.Id
            };
        }
    }
}
