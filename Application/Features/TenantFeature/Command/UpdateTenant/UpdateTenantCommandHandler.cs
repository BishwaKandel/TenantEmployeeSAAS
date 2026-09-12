using Application.Features.TenantFeature.Command.CreateTenant;
using Application.Interfaces.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Command.UpdateTenant
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, ResponseModel<string>>
    {
        private readonly IRepositoryManager _repo;

        public UpdateTenantCommandHandler(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<ResponseModel<string>> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<string>();
            var tenant = await _repo.tenantRepository.FindByCondition(x=> x.Id == request.Id , false).FirstOrDefaultAsync();

            if (tenant == null)
            {
                response.Success = false;
                response.Message = "Tenant not found.";
                return response;
            }

            tenant.Name = request.Name;
            tenant.EmailAddress = request.EmailAddress;
            response.Success = true;
            response.Message = "Tenant updated successfully.";
            return response;

        }
    }
}
