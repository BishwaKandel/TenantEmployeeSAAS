using Domain.DTO.TenantDataTransferObjects;
using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Query.GetAllTenants
{
    public class GetAllTenantsQuery : IRequest<ResponseModel<List<TenantDto>>>
    {
    }
}
