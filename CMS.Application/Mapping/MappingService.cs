using AutoMapper;

namespace CMS.Application.Mapping
{
    public partial class MappingService : Profile
    {
        public MappingService()
        {
            MapUser();
            MapPermission();
            MapAction();
            MapStatus();
            MapCategory();
            MapAttachment();
            MapFile();
        }
    }
}