﻿using System.Diagnostics.CodeAnalysis;

namespace CMS.Common.DTO.Identity.User
{
    [ExcludeFromCodeCoverage]
    public class ActiveDirectoryUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LogonName { get; set; }

        public string Principal { get; set; }

        public string EmployeeId { get; set; }

        public string NameAr { get; set; }
    }
}
