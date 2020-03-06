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

            modelBuilder.Entity<Flights>().HasData(
                new Flights
                {
                    ID = new System.Guid("c419635b-d2af-4e7e-a799-53c1f1b80399"),
                    StartLocation = "Sofia",
                    Destination = "Dubai",
                    StartTime = System.DateTime.Parse("2020-06-01 14:10"),
                    EndTime = System.DateTime.Parse("2020-06-01 18:23"),
                    PilotName = "Harold Estrada",
                    PlaneType = "Boeing 737-800",
                    FlightNumber = "FZ1758",
                    CustomerCapacity = 24,
                    CustomerCapacityBussinessClass = 4
                },
                new Flights
                {
                    ID = new System.Guid("d393eb41-6567-494b-9ab3-57177b09a903"),
                    StartLocation = "Sofia",
                    Destination = "Frankfurt",
                    StartTime = System.DateTime.Parse("2020-06-01 14:15"),
                    EndTime = System.DateTime.Parse("2020-06-01 16:40"),
                    PilotName = "Douglas Fernandez",
                    PlaneType = "Airbus A320",
                    FlightNumber = "LH1427",
                    CustomerCapacity = 12,
                    CustomerCapacityBussinessClass = 2
                },
                new Flights
                {
                    ID = new System.Guid("7d377536-59e5-405a-a82f-b2a9f3ab01b9"),
                    StartLocation = "Sofia",
                    Destination = "Eindhoven",
                    StartTime = System.DateTime.Parse("2020-06-01 14:35"),
                    EndTime = System.DateTime.Parse("2020-06-01 16:40"),
                    PilotName = "Heather Hudson",
                    PlaneType = "Airbus A320",
                    FlightNumber = "W64325",
                    CustomerCapacity = 96,
                    CustomerCapacityBussinessClass = 6
                },
                new Flights
                {
                    ID = new System.Guid("4ff1c64b-4a57-4bce-9f14-bc678e5a0516"),
                    StartLocation = "Sofia",
                    Destination = "Warsaw",
                    StartTime = System.DateTime.Parse("2020-06-01 14:40"),
                    EndTime = System.DateTime.Parse("2020-06-01 16:40"),
                    PilotName = "Ronald Howard",
                    PlaneType = "Embraer 190",
                    FlightNumber = "LO632",
                    CustomerCapacity = 56,
                    CustomerCapacityBussinessClass = 0
                },
                new Flights
                {
                    ID = new System.Guid("4d977c66-3bb0-48c5-9b1a-ff6924ebcf09"),
                    StartLocation = "Sofia",
                    Destination = "Geneve",
                    StartTime = System.DateTime.Parse("2020-06-01 14:45"),
                    EndTime = System.DateTime.Parse("2020-06-01 16:40"),
                    PilotName = "Lori Sandoval",
                    PlaneType = "Airbus A320",
                    FlightNumber = "W64319",
                    CustomerCapacity = 105,
                    CustomerCapacityBussinessClass = 10
                },
                new Flights
                {
                    ID = new System.Guid("c46ecd37-f359-453c-b47c-054c3ecadc4b"),
                    StartLocation = "Sofia",
                    Destination = "Varna",
                    StartTime = System.DateTime.Parse("2020-06-01 14:45"),
                    EndTime = System.DateTime.Parse("2020-06-01 15:43"),
                    PilotName = "Joe White",
                    PlaneType = "Airbus A320",
                    FlightNumber = "FB977",
                    CustomerCapacity = 138,
                    CustomerCapacityBussinessClass = 12
                },
                new Flights
                {
                    ID = new System.Guid("09ca8671-073a-4391-a4c9-a5e6462dc41c"),
                    StartLocation = "Sofia",
                    Destination = "Belgrade",
                    StartTime = System.DateTime.Parse("2020-06-01 16:00"),
                    EndTime = System.DateTime.Parse("2020-06-01 16:55"),
                    PilotName = "Diane McDonald",
                    PlaneType = "ATR 72",
                    FlightNumber = "JU123",
                    CustomerCapacity = 72,
                    CustomerCapacityBussinessClass = 0
                },
                new Flights
                {
                    ID = new System.Guid("a13790d0-d926-494d-9fc8-7530601cc078"),
                    StartLocation = "Sofia",
                    Destination = "Madrid",
                    StartTime = System.DateTime.Parse("2020-06-01 16:55"),
                    EndTime = System.DateTime.Parse("2020-06-01 18:40"),
                    PilotName = "Steven Carroll",
                    PlaneType = "Boeing 737",
                    FlightNumber = "FR6409A",
                    CustomerCapacity = 114,
                    CustomerCapacityBussinessClass = 16
                },
                new Flights
                {
                    ID = new System.Guid("35e9fb5e-fdb7-4a80-84da-46e9ea10a4e8"),
                    StartLocation = "Sofia",
                    Destination = "Paris Beauvais",
                    StartTime = System.DateTime.Parse("2020-06-01 17:00"),
                    EndTime = System.DateTime.Parse("2020-06-01 19:10"),
                    PilotName = "Eric Hunt",
                    PlaneType = "Boeing 737",
                    FlightNumber = "FR6793",
                    CustomerCapacity = 114,
                    CustomerCapacityBussinessClass = 12
                },
                new Flights
                {
                    ID = new System.Guid("d9fd06c8-500c-443f-a7b4-2218158d99bd"),
                    StartLocation = "Sofia",
                    Destination = "Brussels",
                    StartTime = System.DateTime.Parse("2020-06-01 17:55"),
                    EndTime = System.DateTime.Parse("2020-06-01 20:05"),
                    PilotName = "Phillip Vasquez",
                    PlaneType = "Embraer 190",
                    FlightNumber = "FB407A",
                    CustomerCapacity = 180,
                    CustomerCapacityBussinessClass = 2
                },
                new Flights
                {
                    ID = new System.Guid("cd0a2f6e-5ae8-4024-87dc-3cf5e0aee659"),
                    StartLocation = "Sofia",
                    Destination = "Athens",
                    StartTime = System.DateTime.Parse("2020-06-01 19:30"),
                    EndTime = System.DateTime.Parse("2020-06-01 22:40"),
                    PilotName = "Donald Chavez",
                    PlaneType = "Airbus A320",
                    FlightNumber = "A3983",
                    CustomerCapacity = 85,
                    CustomerCapacityBussinessClass = 4
                },
                new Flights
                {
                    ID = new System.Guid("cb49636c-8513-4e92-a37f-db02d8d418d0"),
                    StartLocation = "Sofia",
                    Destination = "Nuremberg",
                    StartTime = System.DateTime.Parse("2020-06-01 20:30"),
                    EndTime = System.DateTime.Parse("2020-06-01 22:45"),
                    PilotName = "Sean Bell",
                    PlaneType = "Airbus A320",
                    FlightNumber = "W64343",
                    CustomerCapacity = 112,
                    CustomerCapacityBussinessClass = 8
                },
                new Flights
                {
                    ID = new System.Guid("f76be08d-9b04-4a57-9268-d33617fce3a2"),
                    StartLocation = "Sofia",
                    Destination = "Vienna",
                    StartTime = System.DateTime.Parse("2020-06-01 21:00"),
                    EndTime = System.DateTime.Parse("2020-06-01 23:15"),
                    PilotName = "Elizabeth Collins",
                    PlaneType = "Embraer 195",
                    FlightNumber = "OS794",
                    CustomerCapacity = 132,
                    CustomerCapacityBussinessClass = 14
                }
            );
        }
    }
}
