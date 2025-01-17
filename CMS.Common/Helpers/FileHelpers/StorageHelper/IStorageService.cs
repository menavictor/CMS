﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CMS.Common.DTO.Common.File;

namespace CMS.Common.Helpers.FileHelpers.StorageHelper
{
    public interface IStorageService
    {
        Task<List<FileDto>> StoreToSharedFolder(IFormFileCollection files, string path , string appCode);
        Task<object> StoreBytes(UploadRequestDto dto, string path , string appCode);
        Task<object> DownLoad(string url, string path);
        Task<bool> Delete(string path);
        Task<IEnumerable<object>> GetDirectoriesAsync();

    }
}
