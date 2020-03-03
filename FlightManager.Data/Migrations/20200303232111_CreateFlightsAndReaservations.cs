using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class CreateFlightsAndReaservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ReservationNumbers_seq",
                schema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StartLocation = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UniqePlaneNumber = table.Column<int>(nullable: false),
                    PilotName = table.Column<string>(nullable: true),
                    CustomerCapacity = table.Column<int>(nullable: false),
                    CustomerCapacityBussinessClass = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ReservationNumber = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ReservationNumbers_seq"),
                    FlightID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservations_Flight_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flight",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationPersons",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SSN = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    TicketType = table.Column<string>(nullable: true),
                    IsBussinesClass = table.Column<bool>(nullable: false),
                    ReservationID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationPersons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReservationPersons_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82f3efb2-c932-48e5-b39f-c9c64f5cb103", "5a894841-7a7a-4b81-8e70-2798e3b6e82e", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0efa7782-607f-4353-a482-e300d6bef13b", "22675944-b0c9-48b6-a957-c7a39dd1b1ab", "Employee", "employee" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SSN", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7b4cdf1a-54a6-4961-9b0e-defad96b79a0", 0, null, "f29ad69b-fe17-48ac-97dd-b052bb28a823", "admin@dev.local", true, null, null, false, null, "admin@dev.local", "admin@dev.local", "AQAAAAEAACcQAAAAEGYDrYT6IdaefKcl1x1rGxowIY9wPSkjH+kn3pXkwFjvdOyJBSSr5YK+OmqmvbhxbQ==", null, false, null, "b4f3ee4d-5887-4c13-a6b4-27e2232c68fd", false, "admin@dev.local" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "7b4cdf1a-54a6-4961-9b0e-defad96b79a0", "82f3efb2-c932-48e5-b39f-c9c64f5cb103" });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationPersons_ReservationID",
                table: "ReservationPersons",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightID",
                table: "Reservations",
                column: "FlightID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationPersons");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropSequence(
                name: "ReservationNumbers_seq",
                schema: "dbo");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0efa7782-607f-4353-a482-e300d6bef13b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "7b4cdf1a-54a6-4961-9b0e-defad96b79a0", "82f3efb2-c932-48e5-b39f-c9c64f5cb103" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f3efb2-c932-48e5-b39f-c9c64f5cb103");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b4cdf1a-54a6-4961-9b0e-defad96b79a0");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "AspNetUsers");
        }
    }
}
