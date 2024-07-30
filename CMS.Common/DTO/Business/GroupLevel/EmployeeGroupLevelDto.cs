using System;

namespace CMS.Common.DTO.Business.GroupLevel
{
    public class EmployeeGroupLevelDto
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public Guid GroupLevelId { get; set; }
        public GroupLevelDto GroupLevel { get; set; }
    }
}
