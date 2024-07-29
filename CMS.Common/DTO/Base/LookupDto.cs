using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CMS.Common.Core;

namespace CMS.Common.DTO.Base
{
    [ExcludeFromCodeCoverage]
    public class LookupDto<T> : IEntityDto<T>
    {
        public T Id { get; set; }


        [Required]
        public string NameEn { get; set; }


        [Required]
        public string NameAr { get; set; }


        [Required]
        public string Code { get; set; }


        public DateTime? CreatedDate { get; set; }


        public DateTime? ModifiedDate { get;set; }
    }
}
