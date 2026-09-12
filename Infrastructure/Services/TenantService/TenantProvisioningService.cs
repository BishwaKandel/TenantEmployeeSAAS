using Application.Interfaces.TenantServices;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Services.TenantService
{
    public class TenantProvisioningService : ITenantProvisioningService
    {
        private readonly IConfiguration _configuration;

        public TenantProvisioningService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BuildTenantConnectionString(string tenantCode)
        {
            var baseConn = _configuration.GetConnectionString("TenantDbBase");

            var builder = new NpgsqlConnectionStringBuilder(baseConn)
            {
                Database = $"Tenant_{tenantCode}"
            };

            return builder.ConnectionString;
        }


        public async Task CreateDatabaseAsync(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var databaseName = builder.Database;

            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("The connection string must contain a database name.");

            builder.Database = "postgres";

            await using var connection = new NpgsqlConnection(builder.ConnectionString);
            await connection.OpenAsync();

            var commandBuilder = new NpgsqlCommandBuilder();
            var quotedDatabaseName = commandBuilder.QuoteIdentifier(databaseName);

            await using var command = new NpgsqlCommand($"CREATE DATABASE {quotedDatabaseName}", connection);
            await command.ExecuteNonQueryAsync();
        }

        public async Task MigrateTenantDatabaseAsync(string connectionString)
        {
            var options = new DbContextOptionsBuilder<TenantDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            await using var tenantDb = new TenantDbContext(options);
            await tenantDb.Database.MigrateAsync();
        }
    }
}

