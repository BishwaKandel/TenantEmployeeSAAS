using Application.Features.TenantFeature.Command.CreateTenant;
using Application.Interfaces.Base;
using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.TenantFeature.Command.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, ResponseModel<string>>
    {
        private readonly IRepositoryManager _repo;

        public DeleteTenantCommandHandler(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<string>> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _repo.tenantRepository.FindByCondition(x => x.Id == request.Id, false).FirstOrDefaultAsync();
            if(tenant == null)
            {
                return new ResponseModel<string>
                {
                    Success = false,
                    Message = "Tenant not found",
                    Data = null
                };
            }
            tenant.IsDeleted = true;
            return new ResponseModel<string>
            {
                Success = true,
                Message = "Tenant deleted successfully",
                Data = tenant.Id
            };

        }
    }
}
