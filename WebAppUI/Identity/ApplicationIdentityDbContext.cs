using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUI.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {            
                new IdentityRole
                {
                    Name="SysAdmin"
                },
                new IdentityRole {
                    Name = "Admin",
                },
                new IdentityRole
                {
                    Name= "Customer"
                }
            });
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    UserName = "admin",
                    Email="admin@gmail.com",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "12345Test."),
                },
                new IdentityUser
                {
                    UserName = "sysadmin",
                    Email="sysadmin@gmail.com",
                    NormalizedUserName = "SYSADMIN",
                    PasswordHash = hasher.HashPassword(null, "12345Test"),
                },
                new IdentityUser
                {
                    UserName = "customer",
                    Email = "customer1@gmail.com",
                    NormalizedUserName = "CUSTOMER",
                    PasswordHash = hasher.HashPassword(null, "12345Test"),
                }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = Convert.ToString(3),
                UserId = Convert.ToString(2)
            });
        }
       
    }

    
}