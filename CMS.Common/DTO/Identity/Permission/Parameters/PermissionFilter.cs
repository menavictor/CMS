using System.Diagnostics.CodeAnalysis;
using CMS.Common.DTO.Base;

namespace CMS.Common.DTO.Identity.Permission.Parameters
{
    [ExcludeFromCodeCoverage]
    public class PermissionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
