using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeature.Command.CreateTenant
{
    public class CreateTenantCommand : IRequest<ResponseModel<string>>
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
