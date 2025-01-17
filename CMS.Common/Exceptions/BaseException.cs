﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }
        public BaseException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
