using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addimagepath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image-path",
                table: "medical-staff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "adv-images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagepath = table.Column<string>(name: "image-path", type: "nvarchar(max)", nullable: false),
                    userid = table.Column<int>(name: "user-id", type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adv-images", x => x.id);
                    table.ForeignKey(
                        name: "FK_adv-images_user_user-id",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adv-images_user-id",
                table: "adv-images",
                column: "user-id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adv-images");

            migrationBuilder.DropColumn(
                name: "image-path",
                table: "medical-staff");
        }
    }
}
