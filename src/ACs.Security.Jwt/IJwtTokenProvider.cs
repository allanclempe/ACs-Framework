﻿using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ACs.Security.Jwt
{
    public interface IJwtTokenProvider
    {
        JwtSecurityTokenHandler Handler { get; }
        RSACryptoServiceProvider Provider { get; }
        RsaSecurityKey Key { get; }
        string Audience { get; }
        string Issuer { get; }
        string GetToken(DateTime? notBefore = null, DateTime? expiress = null, params Claim[] claims);
    }
}
