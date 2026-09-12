using Application.Interfaces.TenantServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.TenantService
{
    public class CurrentTenantService : ICurrentTenantService
    {
        public string? TenantId { get; set; }
        public string? ConnectionString { get; set; }
    }
}
