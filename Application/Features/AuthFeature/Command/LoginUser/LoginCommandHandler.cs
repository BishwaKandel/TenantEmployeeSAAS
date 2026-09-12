using Application.Interfaces.AuthServices;
using Domain.DTO.LoginDataTransferObjects;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.AuthFeature.Command.LoginUser
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseModel<TokenResponseDto>>
    {
        private readonly UserManager<Users> _userManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<Users> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ResponseModel<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<TokenResponseDto>();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid username or password.";
                return response;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                response.Success = false;
                response.Message = "Invalid username or password.";
                return response;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            if (role == null)
            {
                response.Success = false;
                response.Message = "User has no assigned role.";
                return response;
            }

            var (token, expiresAtUtc) = _tokenService.GenerateToken(user, role);

            response.Success = true;
            response.Message = "Login successful.";
            response.Data = new TokenResponseDto
            {
                Token = token,
                ExpiresAtUtc = expiresAtUtc,
                UserName = user.UserName!,
                Role = role,
                TenantId = user.TenantId,
            };

            return response;
        }
    }
}
