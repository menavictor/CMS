using System.Diagnostics.CodeAnalysis;
using CMS.Common.DTO.Base;

namespace CMS.Common.DTO.Lookup.Category.Parameters
{
    [ExcludeFromCodeCoverage]
    public class CategoryFilter : MainFilter
    {
        public int? Id { get; set; }
    }
}
