using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.EmployeeFeature.Command.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<ResponseModel<string>>
    {
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

    }
}
