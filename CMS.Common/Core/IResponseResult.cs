﻿using System;
using System.Net;

namespace CMS.Common.Core
{
    public interface IResponseResult : IFinalResult
    {
        IFinalResult PostResult(object result = null,
            HttpStatusCode status = HttpStatusCode.BadRequest, Exception exception = null,
            string message = null);
    }
}
