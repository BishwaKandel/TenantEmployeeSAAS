using Application.Interfaces.Base;
using Domain.DTO.TenantDataTransferObjects;
using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Query.GetAllTenants
{
    public class GetAllTenantsQueryHandler : IRequestHandler<GetAllTenantsQuery, ResponseModel<List<TenantDto>>>
    {
        private readonly IRepositoryManager _repoManager;
    

        public GetAllTenantsQueryHandler(IRepositoryManager repoManager)
        {
            _repoManager = repoManager;
        }

        public async Task<ResponseModel<List<TenantDto>>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<List<TenantDto>>();

            var tenants = await _repoManager.tenantRepository.GetListAsync();

            List<TenantDto> tenantDto = tenants.Select(tenant => new TenantDto
            {
                TenantId = tenant.Id,
                DbConnStr   = tenant.DbConnStr,
                EmailAddress = tenant.EmailAddress,
                IsDeleted   = tenant.IsDeleted,
                Name   = tenant.Name
            }).ToList();

            return new ResponseModel<List<TenantDto>>
            {
                Success = true,
                Message = "Tenants retrieved successfully.",
                Data = tenantDto
            };



        }
    
    }
}
