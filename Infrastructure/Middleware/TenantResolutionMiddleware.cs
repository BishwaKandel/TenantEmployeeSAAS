using Application.Interfaces.TenantServices;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Middleware
{
    // Infrastructure/Middleware/TenantResolutionMiddleware.cs
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ICurrentTenantService currentTenant,
            MasterDbContext masterDb,
            IMemoryCache cache)
        {
            var tenantIdClaim = context.User.FindFirst("TenantId")?.Value;

            if (!string.IsNullOrEmpty(tenantIdClaim))
            {
                var cacheKey = $"tenant-connstr:{tenantIdClaim}";

                var connectionString = await cache.GetOrCreateAsync(cacheKey, async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);

                    var tenant = await masterDb.Tenants
                        .AsNoTracking()
                        .FirstOrDefaultAsync(t => t.TenantId == tenantIdClaim);

                    return tenant?.DbConnStr; // null if tenant not found — see below
                });

                if (string.IsNullOrEmpty(connectionString))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Invalid tenant.");
                    return;
                }

                currentTenant.TenantId = tenantIdClaim;
                currentTenant.ConnectionString = connectionString;
            }

            await _next(context);
        }
    }
}
