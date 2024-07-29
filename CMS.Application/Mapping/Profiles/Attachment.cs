using CMS.Common.DTO.Business.Attachment;
using CMS.Domain.Entities.Business;

// ReSharper disable once CheckNamespace
namespace CMS.Application.Mapping
{
    public partial class MappingService
    {
        public void MapAttachment()
        {
            CreateMap<Attachment, AttachmentDto>()
                .ReverseMap();

            CreateMap<Attachment, AddAttachmentDto>()
                .ReverseMap();

            CreateMap<Attachment, EditAttachmentDto>()
                .ReverseMap();
        }
    }
}