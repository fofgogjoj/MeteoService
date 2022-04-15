using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicSun.Migrations
{
    public partial class _initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VV",
                table: "WeatherEntities",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Air",
                table: "WeatherEntities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "VV",
                table: "WeatherEntities",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Air",
                table: "WeatherEntities",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
