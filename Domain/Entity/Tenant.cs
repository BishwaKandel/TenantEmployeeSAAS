using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Tenant
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string TenantId { get; set; } = null!;

        public string DbConnStr { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
