using Domain.Enumerations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Users : IdentityUser
    {
        public string? TenantId { get; set; } 
        public Tenant? Tenant { get; set; }
    }
}
