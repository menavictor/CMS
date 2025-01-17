﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace CMS.Common.Helpers.HttpClient
{
    [ExcludeFromCodeCoverage]
    public class MicroServicesUrls
    {
        private readonly IConfiguration _configuration;
        private readonly string _userManagerBaseUrl;
        private readonly string _fileManagerBaseUrl;
        public MicroServicesUrls(IConfiguration configuration)
        {
            _configuration = configuration;
            _fileManagerBaseUrl = _configuration["MicroServicesBaseUrl:FileManager"];
            _userManagerBaseUrl = _configuration["MicroServicesBaseUrl:UserManager"];
        }
        /* File Service Urls */
        public string DownloadFile => _fileManagerBaseUrl + _configuration["MicroServicesEndPoints:FileManager:Download"];
        public string DownloadFileWithAppCode => _fileManagerBaseUrl + _configuration["MicroServicesEndPoints:FileManager:DownloadWithAppCode"];
        public string GenerateToken => _fileManagerBaseUrl + _configuration["MicroServicesEndPoints:FileManager:GenerateToken"];
        public string GenerateTokenWithClaims => _fileManagerBaseUrl +
                                       _configuration["MicroServicesEndPoints:FileManager:GenerateTokenWithClaims"];
        /* User Service Urls */
        public string GetEmployeesPhonesByRoleCode => _userManagerBaseUrl + _configuration["MicroServicesEndPoints:UserManager:GetEmployeesPhonesByRoleCode"];
    }
}
