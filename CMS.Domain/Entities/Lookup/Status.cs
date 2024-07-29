using CMS.Domain.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Domain.Entities.Lookup
{
    [ExcludeFromCodeCoverage]
    public class Status : Lookup<int>
    {
        public string EntityName { get; set; }

        public string CssClass { get; set; }
    }
}
