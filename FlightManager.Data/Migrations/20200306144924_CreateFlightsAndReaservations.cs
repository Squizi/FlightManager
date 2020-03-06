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
                    PlaneType = table.Column<string>(nullable: true),
                    FlightNumber = table.Column<string>(nullable: true),
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
                values: new object[,]
                {
                    { "82f3efb2-c932-48e5-b39f-c9c64f5cb103", "427a9821-b51a-49f9-838d-5244e32e40d5", "Admin", "admin" },
                    { "0efa7782-607f-4353-a482-e300d6bef13b", "230a709c-b6bc-4728-9e1b-48e2e470b233", "Employee", "employee" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SSN", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7b4cdf1a-54a6-4961-9b0e-defad96b79a0", 0, null, "21f682df-9a01-4cbc-b7d7-cb533c2a722f", "admin@dev.local", true, null, null, false, null, "admin@dev.local", "admin@dev.local", "AQAAAAEAACcQAAAAEGoOiDZ4NOJl0WTeq3TYbzK3pOhHoVfRDSZPrvbsXVnTI76FvkD2gL/UMj9uLP/QQQ==", null, false, null, "b3947a52-3437-4540-885f-0f60ddb24124", false, "admin@dev.local" });

            migrationBuilder.InsertData(
                table: "Flight",
                columns: new[] { "ID", "CustomerCapacity", "CustomerCapacityBussinessClass", "Destination", "EndTime", "FlightNumber", "PilotName", "PlaneType", "StartLocation", "StartTime" },
                values: new object[,]
                {
                    { new Guid("c419635b-d2af-4e7e-a799-53c1f1b80399"), 24, 4, "Dubai", new DateTime(2020, 6, 1, 18, 23, 0, 0, DateTimeKind.Unspecified), "FZ1758", "Harold Estrada", "Boeing 737-800", "Sofia", new DateTime(2020, 6, 1, 14, 10, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d393eb41-6567-494b-9ab3-57177b09a903"), 12, 2, "Frankfurt", new DateTime(2020, 6, 1, 16, 40, 0, 0, DateTimeKind.Unspecified), "LH1427", "Douglas Fernandez", "Airbus A320", "Sofia", new DateTime(2020, 6, 1, 14, 15, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7d377536-59e5-405a-a82f-b2a9f3ab01b9"), 96, 6, "Eindhoven", new DateTime(2020, 6, 1, 16, 40, 0, 0, DateTimeKind.Unspecified), "W64325", "Heather Hudson", "Airbus A320", "Sofia", new DateTime(2020, 6, 1, 14, 35, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4ff1c64b-4a57-4bce-9f14-bc678e5a0516"), 56, 0, "Warsaw", new DateTime(2020, 6, 1, 16, 40, 0, 0, DateTimeKind.Unspecified), "LO632", "Ronald Howard", "Embraer 190", "Sofia", new DateTime(2020, 6, 1, 14, 40, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4d977c66-3bb0-48c5-9b1a-ff6924ebcf09"), 105, 10, "Geneve", new DateTime(2020, 6, 1, 16, 40, 0, 0, DateTimeKind.Unspecified), "W64319", "Lori Sandoval", "Airbus A320", "Sofia", new DateTime(2020, 6, 1, 14, 45, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c46ecd37-f359-453c-b47c-054c3ecadc4b"), 138, 12, "Varna", new DateTime(2020, 6, 1, 15, 43, 0, 0, DateTimeKind.Unspecified), "FB977", "Joe White", "Airbus A320", "Sofia", new DateTime(2020, 6, 1, 14, 45, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("09ca8671-073a-4391-a4c9-a5e6462dc41c"), 72, 0, "Belgrade", new DateTime(2020, 6, 1, 16, 55, 0, 0, DateTimeKind.Unspecified), "JU123", "Diane McDonald", "ATR 72", "Sofia", new DateTime(2020, 6, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a13790d0-d926-494d-9fc8-7530601cc078"), 114, 16, "Madrid", new DateTime(2020, 6, 1, 18, 40, 0, 0, DateTimeKind.Unspecified), "FR6409A", "Steven Carroll", "Boeing 737", "Sofia", new DateTime(2020, 6, 1, 16, 55, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("35e9fb5e-fdb7-4a80-84da-46e9ea10a4e8"), 114, 12, "Paris Beauvais", new DateTime(2020, 6, 1, 19, 10, 0, 0, DateTimeKind.Unspecified), "FR6793", "Eric Hunt", "Boeing 737", "Sofia", new DateTime(2020, 6, 1, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d9fd06c8-500c-443f-a7b4-2218158d99bd"), 180, 2, "Brussels", new DateTime(2020, 6, 1, 20, 5, 0, 0, DateTimeKind.Unspecified), "FB407A", "Phillip Vasquez", "Embraer 190", "Sofia", new DateTime(2020, 6, 1, 17, 55, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cd0a2f6e-5ae8-4024-87dc-3cf5e0aee659"), 85, 4, "Athens", new DateTime(2020, 6, 1, 22, 40, 0, 0, DateTimeKind.Unspecified), "A3983", "Donald Chavez", "Airbus A320", "Sofia", new DateTime(2020, 6, 1, 19, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cb49636c-8513-4e92-a37f-db02d8d418d0"), 112, 8, "Nuremberg", new DateTime(2020, 6, 1, 22, 45, 0, 0, DateTimeKind.Unspecified), "W64343", "Sean Bell", "Airbus A320", "Sofia", new DateTime(2020, 6, 1, 20, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f76be08d-9b04-4a57-9268-d33617fce3a2"), 132, 14, "Vienna", new DateTime(2020, 6, 1, 23, 15, 0, 0, DateTimeKind.Unspecified), "OS794", "Elizabeth Collins", "Embraer 195", "Sofia", new DateTime(2020, 6, 1, 21, 0, 0, 0, DateTimeKind.Unspecified) }
                });

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
