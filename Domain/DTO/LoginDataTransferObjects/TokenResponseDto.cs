using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.LoginDataTransferObjects
{
    public class TokenResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? TenantId { get; set; }   
    }
}
