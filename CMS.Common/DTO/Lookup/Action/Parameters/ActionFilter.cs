using System.Diagnostics.CodeAnalysis;
using CMS.Common.DTO.Base;

namespace CMS.Common.DTO.Lookup.Action.Parameters
{
    [ExcludeFromCodeCoverage]
    public class ActionFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
