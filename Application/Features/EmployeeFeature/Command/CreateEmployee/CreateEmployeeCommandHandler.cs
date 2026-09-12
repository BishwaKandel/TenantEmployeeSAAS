using Application.Features.TenantFeature.Command.CreateTenant;
using Application.Interfaces.Base;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.EmployeeFeature.Command.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ResponseModel<string>>
    {
        private readonly IRepositoryManager _repo;

        public CreateEmployeeCommandHandler(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repo.employeeRepository.FindByCondition(x=> x.EmailAddress == request.EmailAddress , false).FirstOrDefaultAsync();
            if (employee != null)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Employee already exists",
                    Data = null
                };
            }
            Employee emp = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                FullName = request.FullName,
                EmailAddress = request.EmailAddress
            };
            await _repo.employeeRepository.AddAsync(emp);
            await _repo.SaveAsync();
            return new ResponseModel<string>
            {
                Success = true,
                Data = emp.Id.ToString(),
                Message = "Employee created successfully"
            };


            }
    }
}
