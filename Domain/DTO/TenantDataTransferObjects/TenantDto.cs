using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.TenantDataTransferObjects
{
    public class TenantDto
    {
        public string Name { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

        public string TenantId { get; set; } = null!;
        public string DbConnStr { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
