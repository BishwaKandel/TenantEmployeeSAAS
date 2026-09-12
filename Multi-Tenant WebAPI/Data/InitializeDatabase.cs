using Domain.Entity;
using Domain.Enumerations;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Multi_Tenant_WebAPI.Data
{
    public class InitializeDatabase : IDbInitializer
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MasterDbContext _db;
        private readonly IServiceProvider _serviceProvider;


        public InitializeDatabase(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, MasterDbContext db, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _serviceProvider = serviceProvider;
        }
        public async Task Initialize()
        {
            try
            {
                try
                {
                    if (_db.Database.GetPendingMigrations().Any())
                    {
                        await _db.Database.MigrateAsync();
                    }
                }
                catch (Exception ex)
                {
                    // at minimum log this — a swallowed migration failure here
                    // means every subsequent line runs against a schema that
                    // might not match your entities
                }

                var roles = new List<string>
        {
            UserRole.Employee.ToString(),
            UserRole.Admin.ToString(),
            UserRole.SuperAdmin.ToString()
        };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                if (await _db.Users.FirstOrDefaultAsync(u => u.UserName == "assessment@yopmail.com") == null)
                {
                    var user = new Users
                    {
                        UserName = "assessment@yopmail.com",
                        Email = "assessment@yopmail.com",
                        TenantId = null,
                    };

                    var result = await _userManager.CreateAsync(user, "Tester@123");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, UserRole.SuperAdmin.ToString());
                    }
                }

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
