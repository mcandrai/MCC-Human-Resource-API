﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HumanResourceAPI.ModelView
{
    public class JwtToken
    {
        public HttpStatusCode status { get; set; }
        public int code { get; set; }
        public string idToken { get; set; }
        public string message { get; set; }

    }
}
