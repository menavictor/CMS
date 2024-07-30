using CMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace CMS.Domain.Entities.Business
{
    public class GroupLevel : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public GroupLevel Parent { get; set; }
        public ICollection<EmployeeGroupLevel> EmployeeGroupLevels { get; set; } = new HashSet<EmployeeGroupLevel>();

    }
}
