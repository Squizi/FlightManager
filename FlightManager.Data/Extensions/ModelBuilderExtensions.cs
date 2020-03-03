using FlightManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Declare db relations
        /// </summary>
        /// <param name="builder"></param>
        public static void DbRelations(this ModelBuilder builder)
        {
            builder.Entity<Flights>()
                .HasMany(f => f.Reservations)
                .WithOne(r => r.Flight)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasSequence<int>("ReservationNumbers_seq", schema: "dbo")
                .StartsAt(1)
                .IncrementsBy(1);

            builder.Entity<Reservations>()
            .Property(o => o.ReservationNumber)
            .HasDefaultValueSql("NEXT VALUE FOR dbo.ReservationNumbers_seq");

            builder.Entity<Reservations>()
                .HasMany(r => r.ReservationPersons)
                .WithOne(rp => rp.Reservation)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <summary>
        /// Seed with initial required data like: Admin and Employee roles, Admin user
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            const string ADMIN_ROLE_ID = "82f3efb2-c932-48e5-b39f-c9c64f5cb103";
            const string EMPLOYEE_ROLE_ID = "0efa7782-607f-4353-a482-e300d6bef13b";
            const string ADMIN_ID = "7b4cdf1a-54a6-4961-9b0e-defad96b79a0";

            // Add amin role
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "Admin".ToLower()
                },
                new IdentityRole
                {
                    Id = EMPLOYEE_ROLE_ID,
                    Name = "Employee",
                    NormalizedName = "Employee".ToLower()
                }
            );

            // Add admin user
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = ADMIN_ID,
                    UserName = "admin@dev.local",
                    NormalizedUserName = "admin@dev.local",
                    Email = "admin@dev.local",
                    NormalizedEmail = "admin@dev.local",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "password")
                }    
            );

            // Assign admin user to admin role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
