using CMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMS.Domain.Entities.Business
{
    public class GroupLevel : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public GroupLevel Parent { get; set; }

        public virtual ICollection<EmployeeGroupLevel> EmployeeGroupLevels { get; set; } = new Collection<EmployeeGroupLevel>();



    }
}
