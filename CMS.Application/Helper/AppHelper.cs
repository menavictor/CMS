﻿using System.Diagnostics.CodeAnalysis;
using CMS.Common.Helpers.MediaUploader;

namespace CMS.Application.Helper
{
    [ExcludeFromCodeCoverage]
    public class AppHelper
    {
        private readonly IUploaderConfiguration _uploader;
        private const string FolderName = "Files/";
        public AppHelper(IUploaderConfiguration uploader)
        {
            _uploader = uploader;
        }

        public void RemoveAppFiles<T>(T entity)
        {
            var type = entity.GetType();
            //if (typeof(T) == typeof(EntityHere))
            //{
            //    var property = type.GetProperty("DocumentUrl");
            //    _uploader.RemoveFile(property?.GetValue(entity).ToString(), $"{FolderName}Document");
            //}
        }
    }
}