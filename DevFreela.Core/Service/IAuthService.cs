﻿using System.Globalization;

namespace DevFreela.Core.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
