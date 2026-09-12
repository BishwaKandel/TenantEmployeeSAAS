using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO.EmployeeDataTransferObjects
{
    public class EmployeeDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
