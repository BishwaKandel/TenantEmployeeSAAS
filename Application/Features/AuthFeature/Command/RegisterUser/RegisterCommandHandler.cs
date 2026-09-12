using Application.Interfaces.TenantServices;
using Domain.Entity;
using Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.AuthFeature.Command.RegisterUser
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseModel<string>>
    {
        private readonly UserManager<Users> _userManager;
        private readonly ICurrentTenantService _currentTenant;

        public RegisterCommandHandler(UserManager<Users> userManager, ICurrentTenantService currentTenant)
        {
            _userManager = userManager;
            _currentTenant = currentTenant;
        }

        public async Task<ResponseModel<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<string>();

            if (string.IsNullOrEmpty(_currentTenant.TenantId))
            {
                response.Success = false;
                response.Message = "No tenant context found for the authenticated user.";
                return response;
            }

            var existingUser = await _userManager.FindByEmailAsync(request.EmailAddress);
            if (existingUser != null)
            {
                response.Success = false;
                response.Message = "A user with this email already exists.";
                return response;
            }

            var newUser = new Users
            {
                UserName = request.EmailAddress,
                Email = request.EmailAddress,
                TenantId = _currentTenant.TenantId,   
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = string.Join("; ", result.Errors.Select(e => e.Description));
                return response;
            }

            await _userManager.AddToRoleAsync(newUser, UserRole.Employee.ToString());

            response.Success = true;
            response.Message = "User registered successfully.";
            response.Data = newUser.Id;
            return response;
        }
    }
}
