using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexmodulBackendV2.Migrations
{
    public partial class minorChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ProductionPrice",
                table: "ProductionInformations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductionPrice",
                table: "ProductionInformations",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
