using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Command.DeleteTenant
{
    public class DeleteTenantCommand : IRequest<ResponseModel<string>>
    {
        public string Id { get; set; }
    }
}
