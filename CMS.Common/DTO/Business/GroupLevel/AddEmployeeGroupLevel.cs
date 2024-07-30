using CMS.Common.Core;
using System;

namespace CMS.Common.DTO.Business.GroupLevel
{
    public class AddEmployeeGroupLevel : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public Guid GroupLevelId { get; set; }
    }
}
