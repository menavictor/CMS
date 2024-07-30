using CMS.Common.Core;
using System;

namespace CMS.Common.DTO.Business.GroupLevel
{
    public class EditGroupLevel : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
