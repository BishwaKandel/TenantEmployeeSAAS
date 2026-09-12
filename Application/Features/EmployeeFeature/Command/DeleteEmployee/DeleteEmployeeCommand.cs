using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.EmployeeFeature.Command.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<ResponseModel<string>>
    {
        public string Id { get; set; } = null!;

    }
}
