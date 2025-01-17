﻿using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using CMS.Common.DTO.Base;

namespace CMS.Common.Services
{
    [ExcludeFromCodeCoverage]
    public class ClaimService : IClaimService
    {
        private readonly HttpContext _context;
        protected TokenClaimDto ClaimData { get; set; }
        public ClaimService()
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            _context = httpContextAccessor.HttpContext;
            SetClaims(_context);

        }

        public void SetClaims(HttpContext context)
        {

            var claims = context?.User;
            ClaimData = new TokenClaimDto()
            {
                UserId = claims?.FindFirst(t => t.Type == "UserId")?.Value,
                Email = claims?.FindFirst(t => t.Type == "Email")?.Value
            };
        }
        public string UserId => ClaimData.UserId ?? string.Empty;

        public string Token => _context.Request.Headers["Authorization"];

    }
}
