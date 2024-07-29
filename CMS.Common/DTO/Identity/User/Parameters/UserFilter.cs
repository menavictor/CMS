using System;
using System.Diagnostics.CodeAnalysis;
using CMS.Common.DTO.Base;

namespace CMS.Common.DTO.Identity.User.Parameters
{
    [ExcludeFromCodeCoverage]
    public class UserFilter : MainFilter
    {
        public Guid Id { get; set; }
    }
}
