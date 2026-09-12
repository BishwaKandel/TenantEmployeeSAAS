using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Employee
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;
    }
}
