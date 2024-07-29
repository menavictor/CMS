using System.Diagnostics.CodeAnalysis;
using CMS.Common.DTO.Base;

namespace CMS.Common.DTO.Lookup.Status.Parameters
{
    [ExcludeFromCodeCoverage]
    public class StatusFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
