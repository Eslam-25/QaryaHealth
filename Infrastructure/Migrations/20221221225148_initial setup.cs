using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phonenumber = table.Column<string>(name: "phone-number", type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "donor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    birthdate = table.Column<DateTime>(name: "birth-date", type: "datetime2", nullable: true),
                    lastdonationdate = table.Column<DateTime>(name: "last-donation-date", type: "datetime2", nullable: true),
                    bloodtype = table.Column<int>(name: "blood-type", type: "int", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: true),
                    readytodonor = table.Column<bool>(name: "ready-to-donor", type: "bit", nullable: false),
                    userid = table.Column<int>(name: "user-id", type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donor", x => x.id);
                    table.ForeignKey(
                        name: "FK_donor_user_user-id",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical-staff",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    workingdescription = table.Column<string>(name: "working-description", type: "nvarchar(max)", nullable: true),
                    readytowork = table.Column<bool>(name: "ready-to-work", type: "bit", nullable: false),
                    userid = table.Column<int>(name: "user-id", type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical-staff", x => x.id);
                    table.ForeignKey(
                        name: "FK_medical-staff_user_user-id",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical-staff-service",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    staffid = table.Column<int>(name: "staff-id", type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical-staff-service", x => x.id);
                    table.ForeignKey(
                        name: "FK_medical-staff-service_medical-staff_staff-id",
                        column: x => x.staffid,
                        principalTable: "medical-staff",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "address", "email", "isActive", "name", "password", "phone-number", "role" },
                values: new object[] { 1, null, "admin@heikal.com", true, "Admin", "admin", "123", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_donor_user-id",
                table: "donor",
                column: "user-id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medical-staff_user-id",
                table: "medical-staff",
                column: "user-id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medical-staff-service_staff-id",
                table: "medical-staff-service",
                column: "staff-id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_phone-number",
                table: "user",
                column: "phone-number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "donor");

            migrationBuilder.DropTable(
                name: "medical-staff-service");

            migrationBuilder.DropTable(
                name: "medical-staff");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
