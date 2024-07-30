using CMS.Common.Core;
using System;
using System.Collections.Generic;

namespace CMS.Common.DTO.Business.GroupLevel
{
    public class GroupLevelDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public GroupLevelDto Parent { get; set; }

        public List<EmployeeGroupLevelDto> EmployeeGroupLevels { get; set; }




    }
}
