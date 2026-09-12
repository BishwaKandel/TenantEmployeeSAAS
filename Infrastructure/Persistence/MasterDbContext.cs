using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
        public class MasterDbContext : IdentityDbContext<Users>
        {
            public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }

            public DbSet<Tenant> Tenants { get; set; } = null!;

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                builder.Entity<Tenant>(b =>
                {
                    b.HasIndex(t => t.TenantId).IsUnique();
                    b.Property(t => t.TenantId).HasMaxLength(4);
                });

                builder.Entity<Users>()
                    .HasOne(u => u.Tenant)
                    .WithMany()
                    .HasForeignKey(u => u.TenantId)
                    .IsRequired(false);
            }
        }
    }

