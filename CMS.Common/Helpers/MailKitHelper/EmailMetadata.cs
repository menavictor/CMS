﻿using System.Diagnostics.CodeAnalysis;

namespace CMS.Common.Helpers.MailKitHelper
{
    [ExcludeFromCodeCoverage]
    public class EmailMetadata
    {
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EnableSsl { get; set; }
    }
}
