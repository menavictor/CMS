﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Common.DTO.Common.File
{
    [ExcludeFromCodeCoverage]
    public class UploadResponseDto
    {
        public Guid FileId { get; set; }

        public string AttachmentName { get; set; }

        public string AttachmentExtension { get; set; }

        public string AttachmentSize { get; set; }

        public string AttachmentType { get; set; }
    }
}
