using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3._1.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    HitPoints = table.Column<int>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Defence = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    Fights = table.Column<int>(nullable: false),
                    Victories = table.Column<int>(nullable: false),
                    Defeats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
