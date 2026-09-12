using Application.Interfaces.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common;

namespace Application.Features.EmployeeFeature.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ResponseModel<string>>
    {
        private readonly IRepositoryManager _repo;

        public UpdateEmployeeCommandHandler(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<string>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repo.employeeRepository.FindByCondition(x => x.EmailAddress == request.EmailAddress, false).FirstOrDefaultAsync();
            if (employee == null)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Employee not found",
                    Data = null
                };
            }

            employee.FullName = request.FullName;
            employee.EmailAddress = request.EmailAddress;
            await _repo.employeeRepository.UpdateAsync(employee);
            await _repo.SaveAsync();
            return new ResponseModel<string>
            {
                Success = true,
                Message = "Employee updated successfully",
            };


        }
    }
}
