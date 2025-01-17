﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Common.DTO.Business.Attachment
{
    [ExcludeFromCodeCoverage]
    public class EditAttachmentDto
    {
        public Guid? Id { get; set; }

        public Guid FileId { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public string Size { get; set; }

        public bool IsPublic { get; set; }

        public string AttachmentDisplaySize { get; set; }
    }
}
