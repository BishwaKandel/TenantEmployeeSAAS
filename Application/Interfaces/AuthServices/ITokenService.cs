using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.AuthServices
{
    public interface ITokenService
    {
        (string Token, DateTime ExpiresAtUtc) GenerateToken(Users user, string role);
    }
}
