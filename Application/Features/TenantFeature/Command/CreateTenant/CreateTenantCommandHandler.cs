using Application.Interfaces.Base;
using Application.Interfaces.TenantServices;
using Domain.Entity;
using Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Features.TenantFeature.Command.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ResponseModel<string>>
    {
        private readonly IRepositoryManager _repo;
        private readonly UserManager<Users> _userManager;
        private readonly ITenantProvisioningService _provisioning;

        public CreateTenantCommandHandler(IRepositoryManager repo , UserManager<Users> userManager, ITenantProvisioningService provisioning)
        {
            _repo = repo;
            _userManager = userManager;
            _provisioning = provisioning;
        }

        public async Task<ResponseModel<string>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<string>();
            try
            {
                var existingTenant = await _repo.tenantRepository
                    .FindByCondition(x => x.EmailAddress == request.EmailAddress, false)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingTenant != null)
                {
                    response.Success = false;
                    response.Message = "Tenant already exists.";
                    return response;
                }

                var tenantCode = await GenerateUniqueTenantIdAsync(cancellationToken);
                var connectionString = _provisioning.BuildTenantConnectionString(tenantCode);

                var newTenant = new Tenant
                {
                    Name = request.Name,
                    EmailAddress = request.EmailAddress,
                    TenantId = tenantCode,
                    DbConnStr = connectionString,
                };
                await _provisioning.CreateDatabaseAsync(connectionString);
                await _provisioning.MigrateTenantDatabaseAsync(connectionString);

                await _repo.tenantRepository.AddAsync(newTenant);

                var (adminCreated, adminResult) = await CreateDefaultAdminUserAsync(newTenant, cancellationToken);
                if (!adminCreated)
                {
                    response.Success = false;
                    response.Message = $"Tenant created, but default Admin user failed: {adminResult}";
                    response.Data = newTenant.Id.ToString();
                    return response;
                }

                response.Success = true;
                response.Message = "Tenant created successfully.";
                response.Data = newTenant.Id.ToString();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while creating the tenant: {ex.Message}";
            }
            return response;
        }


        private async Task<string> GenerateUniqueTenantIdAsync(CancellationToken ct)
        {
            string code;
            bool exists;
            do
            {
                code = Guid.NewGuid().ToString("N")[..4].ToUpperInvariant();
                exists = await _repo.tenantRepository
                    .FindByCondition(x => x.TenantId == code, false)
                    .AnyAsync(ct);
            } while (exists);

            return code;
        }

        public async Task<(bool Succeeded, string? ErrorMessage)> CreateDefaultAdminUserAsync(
                            Tenant tenant,
                            CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(tenant.EmailAddress);
            if (existingUser != null)
                return (false, "A user with this email already exists.");

            var adminUser = new Users
            {
                UserName = tenant.EmailAddress,
                Email = tenant.EmailAddress,
                TenantId = tenant.Id,
            };

            //var temporaryPassword = GenerateTemporaryPassword();
            var temporaryPassword = tenant.EmailAddress;

            var result = await _userManager.CreateAsync(adminUser, temporaryPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return (false, errors);
            }

            await _userManager.AddToRoleAsync(adminUser, UserRole.Admin.ToString());

            return (true, temporaryPassword);
        }
    }

}
