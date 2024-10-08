﻿using Ec_Supermercado.Api.DTOs;
using Ec_Supermercado.Api.Models;

namespace Ec_Supermercado.Api.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateToken (LoginDto loginDto);
    }
}
