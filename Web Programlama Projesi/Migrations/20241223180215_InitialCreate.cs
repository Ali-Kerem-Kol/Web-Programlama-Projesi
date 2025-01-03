﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_Programlama_Projesi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WorkingHours = table.Column<string>(type: "text", nullable: false),
                    AppointmentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Expertise = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalonId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<string>(type: "text", nullable: false),
                    EndTime = table.Column<string>(type: "text", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Expertise = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TotalAppointments = table.Column<int>(type: "integer", nullable: false),
                    TotalEarnings = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeSlotId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Salons",
                columns: new[] { "Id", "AppointmentPrice", "Expertise", "Name", "WorkingHours" },
                values: new object[,]
                {
                    { 1, 100m, "Saç Kesimi", "Saç Kesim Salonu", "09:00-17:00" },
                    { 2, 100m, "Güzellik Bakımı", "Güzellik Salonu", "10:00-18:00" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, true, "123456", "Admin", "B221210042@sakarya.edu.tr" },
                    { 2, true, "123", "User", "user" },
                    { 3, true, "123", "Employee", "Ahmet" },
                    { 4, true, "123", "Employee", "Ayşe" },
                    { 5, true, "123", "Employee", "Mehmet" },
                    { 6, true, "123", "Employee", "Fatma" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Expertise", "IsActive", "TotalAppointments", "TotalEarnings", "UserId" },
                values: new object[,]
                {
                    { 1, "Saç Kesimi", true, 0, 0m, 3 },
                    { 2, "Güzellik Bakımı", true, 0, 0m, 4 },
                    { 3, "Masaj", true, 0, 0m, 5 },
                    { 4, "Pedikür/Manikür", true, 0, 0m, 6 }
                });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "EndTime", "IsAvailable", "SalonId", "StartTime" },
                values: new object[,]
                {
                    { 1, "10:00", true, 1, "1 Ocak 09:00" },
                    { 2, "11:00", true, 1, "1 Ocak 10:00" },
                    { 3, "12:00", true, 1, "1 Ocak 11:00" },
                    { 4, "13:00", true, 1, "1 Ocak 12:00" },
                    { 5, "14:00", true, 1, "1 Ocak 13:00" },
                    { 6, "15:00", true, 1, "1 Ocak 14:00" },
                    { 7, "16:00", true, 1, "1 Ocak 15:00" },
                    { 8, "17:00", true, 1, "1 Ocak 16:00" },
                    { 9, "11:00", true, 2, "1 Ocak 10:00" },
                    { 10, "12:00", true, 2, "1 Ocak 11:00" },
                    { 11, "13:00", true, 2, "1 Ocak 12:00" },
                    { 12, "14:00", true, 2, "1 Ocak 13:00" },
                    { 13, "15:00", true, 2, "1 Ocak 14:00" },
                    { 14, "16:00", true, 2, "1 Ocak 15:00" },
                    { 15, "17:00", true, 2, "1 Ocak 16:00" },
                    { 16, "18:00", true, 2, "1 Ocak 17:00" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TimeSlotId",
                table: "Appointments",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_SalonId",
                table: "TimeSlots",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Salons");
        }
    }
}
