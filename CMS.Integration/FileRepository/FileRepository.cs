﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using CMS.Common.Core;
using CMS.Common.Helpers.HttpClient;
using CMS.Common.Helpers.HttpClient.RestSharp;
using TokenDto = CMS.Common.DTO.Common.File.TokenDto;

namespace CMS.Integration.FileRepository
{
    public class FileRepository : IFileRepository
    {

        private readonly IRestSharpClient _restSharpClient;
        private readonly MicroServicesUrls _urls;
        private readonly IConfiguration _configuration;


        public FileRepository(IRestSharpClient restSharpClient, IConfiguration configuration, MicroServicesUrls urls)
        {
            _restSharpClient = restSharpClient;
            _configuration = configuration;
            _urls = urls;
        }

        public async Task<List<TokenDto>> GetTokens(List<Guid> ids)
        {
            var appCode = _configuration["AppCode"];
            var result = await _restSharpClient.SendRequest<ResponseResult>(_urls.GenerateTokenWithClaims + "/" + appCode, Method.Post, ids);
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(JsonConvert.SerializeObject(result.Data));
            return tokens;
        }

    }
}
