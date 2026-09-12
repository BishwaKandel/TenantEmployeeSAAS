using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Command.UpdateTenant
{
    public class UpdateTenantCommand : IRequest<ResponseModel<string>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
