using Domain.DTO.TenantDataTransferObjects;
using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Query.GetTenantById
{
    public record GetTenantByIdQuery(string Id) : IRequest<ResponseModel<TenantDto>>
    {
    }
}
