﻿namespace CMS.Common.Core
{
    public interface IEntityDto<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
