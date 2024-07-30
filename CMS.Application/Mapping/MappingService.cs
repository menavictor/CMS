using AutoMapper;

namespace CMS.Application.Mapping
{
    public partial class MappingService : Profile
    {
        public MappingService()
        {
            #region Business
            MapGroupLevel();
            MapAttachment();
            #endregion

            #region Lookup
            MapAction();
            MapStatus();
            MapCategory();
            #endregion
            #region User
            MapUser();
            MapPermission();
            #endregion
            #region Common
            MapFile();
            #endregion

        }
    }
}