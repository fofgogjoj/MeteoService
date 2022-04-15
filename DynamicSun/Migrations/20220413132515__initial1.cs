using Microsoft.EntityFrameworkCore.Migrations;

namespace DynamicSun.Migrations
{
    public partial class _initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AtmPressure",
                table: "WeatherEntities",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AtmPressure",
                table: "WeatherEntities",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
