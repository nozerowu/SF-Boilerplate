﻿ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Infrastructure.Common.Sms
{
    public class TwilioSmsCredentials
    {
        public string AccountSid { get; set; } = string.Empty;

        public string AuthToken { get; set; } = string.Empty;

        public string FromNumber { get;set;}


    }
}
