using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rpg.Migrations
{
    /// <inheritdoc />
    public partial class userCreateAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TbCharacters",
                table: "TbCharacters");

            migrationBuilder.RenameTable(
                name: "TbCharacters",
                newName: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Characters",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characters",
                table: "Characters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_userId",
                table: "Characters",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_userId",
                table: "Characters",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_userId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Characters",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_userId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "Characters",
                newName: "TbCharacters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbCharacters",
                table: "TbCharacters",
                column: "Id");
        }
    }
}
