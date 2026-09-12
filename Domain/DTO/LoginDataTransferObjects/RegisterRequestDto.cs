using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.LoginDataTransferObjects
{
    public class RegisterRequestDto
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
