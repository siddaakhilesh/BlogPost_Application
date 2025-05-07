using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC_Application.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles (user, admin, superadmin)
            var adminRoleId = "f7d704cd-97ee-4040-9f6f-1202fc5e0380";
            var superAdminRoleId = "f2db4942-df68-41c8-955e-d2a971cb0728";
            var userRoleId = "ee587249-e634-46c2-82f1-d9f7f2a8d99e";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Seed superadmin user
            var superAdminId = "b0a32fe9-a535-4e3d-9c88-7ce6b551bcea";
            var precomputedPasswordHash = "AQAAAAIAAYagAAAAEDTVBPR6DQZQgqzQ+k6n5EX0mjsB4ceRs+XkAVOdwO1jEqauJhU1mW+r9nUwzQwKIA=="; // superAdmin@123

            var superAdminUser = new IdentityUser
            {
                Id = superAdminId,
                UserName = "superadmin@bloggie.com",
                NormalizedUserName = "SUPERADMIN@BLOGGIE.COM",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "SUPERADMIN@BLOGGIE.COM",
                EmailConfirmed = true,
                PasswordHash = precomputedPasswordHash,
                SecurityStamp = "1d9473f5-7bcd-4c2c-b3a2-2a6b4ea5e8e9", // fixed value
                ConcurrencyStamp = "7c62f5f3-27f5-4a9a-9d9a-73c62a14cd7d"  // fixed value
            };

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Assign roles to superadmin user
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = superAdminId },
                new IdentityUserRole<string> { RoleId = superAdminRoleId, UserId = superAdminId },
                new IdentityUserRole<string> { RoleId = userRoleId, UserId = superAdminId }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
