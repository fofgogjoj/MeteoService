using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicSun.Migrations
{
    public partial class _initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    Air = table.Column<int>(nullable: false),
                    Td = table.Column<double>(nullable: false),
                    AtmPressure = table.Column<double>(nullable: false),
                    Direction = table.Column<string>(nullable: true),
                    Speed = table.Column<int>(nullable: false),
                    Cloudiness = table.Column<int>(nullable: false),
                    H = table.Column<int>(nullable: false),
                    VV = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherEntities");
        }
    }
}
