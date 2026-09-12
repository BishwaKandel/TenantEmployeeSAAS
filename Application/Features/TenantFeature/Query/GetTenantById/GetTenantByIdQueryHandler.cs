using Application.Interfaces.Base;
using Domain.DTO.TenantDataTransferObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Query.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, ResponseModel<TenantDto>>
    {
        private readonly IRepositoryManager _repoManager;

        public GetTenantByIdQueryHandler(IRepositoryManager repoManager)
        {
            _repoManager = repoManager;
        }

        public async Task<ResponseModel<TenantDto>> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenants = await  _repoManager.tenantRepository.FindByCondition(x=> x.TenantId == request.Id && !x.IsDeleted , false).FirstOrDefaultAsync();
            if(tenants == null)
            {
                return new ResponseModel<TenantDto>
                {
                    Success = false,
                    Message = "Tenant not found",
                    Data = null
                };
            }
            return new ResponseModel<TenantDto>
            {
                Data = new TenantDto
                {
                    Name = tenants.Name,
                    EmailAddress = tenants.EmailAddress,
                    TenantId = tenants.TenantId,
                    DbConnStr = tenants.DbConnStr,
                    IsDeleted = tenants.IsDeleted
                },
                Message = "Tenant retrieved successfully",
                Success = true
            };

        }
    }
}
