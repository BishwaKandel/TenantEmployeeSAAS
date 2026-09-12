using Domain.DTO.EmployeeDataTransferObjects;
using Domain.DTO.TenantDataTransferObjects;
using MediatR;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.EmployeeFeature.Query.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<ResponseModel<List<EmployeeDto>>>
    {
    }
}
