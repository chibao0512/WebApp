using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    Gen_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gen_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gen_Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.Gen_Id);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    Publisher_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publisher_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Publisher_Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.Publisher_Id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    Book_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Book_Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Book_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Book_Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Book_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gen_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Book_Id);
                    table.ForeignKey(
                        name: "FK_books_genres_Gen_Id",
                        column: x => x.Gen_Id,
                        principalTable: "genres",
                        principalColumn: "Gen_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_Gen_Id",
                table: "books",
                column: "Gen_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.DropTable(
                name: "genres");
        }
    }
}
