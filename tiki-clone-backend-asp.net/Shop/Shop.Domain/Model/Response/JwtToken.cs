﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model.Response
{
    public class JwtToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public JwtToken(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
