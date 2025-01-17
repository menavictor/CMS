﻿using System.Diagnostics.CodeAnalysis;

namespace CMS.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public class SortModel
    {
        public string ColId { get; set; } = "id";
        public string Sort { get; set; } = "asc";
        public string PairAsSqlExpression => $"{ColId} {Sort}";
    }
}
