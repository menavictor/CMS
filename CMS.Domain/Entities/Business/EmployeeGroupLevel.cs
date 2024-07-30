using CMS.Domain.Entities.Base;
using System;

namespace CMS.Domain.Entities.Business
{
    public class EmployeeGroupLevel : BaseEntity<Guid>
    {

        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public Guid GroupLevelId { get; set; }
        public virtual GroupLevel GroupLevel { get; set; }
    }
}
