﻿using System;
using System.Net;

namespace CMS.Common.Core
{
    public interface IFinalResult
    {
        object Data { get; set; }

        HttpStatusCode Status { get; set; }

        string Message { get; set; }

        Exception Exception { get; set; }
    }
}
