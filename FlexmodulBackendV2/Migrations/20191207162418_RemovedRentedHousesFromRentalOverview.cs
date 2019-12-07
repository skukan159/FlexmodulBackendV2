using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexmodulBackendV2.Migrations
{
    public partial class RemovedRentedHousesFromRentalOverview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FmHouses_RentalOverviews_RentalOverviewId",
                table: "FmHouses");

            migrationBuilder.DropIndex(
                name: "IX_FmHouses_RentalOverviewId",
                table: "FmHouses");

            migrationBuilder.DropColumn(
                name: "RentalOverviewId",
                table: "FmHouses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RentalOverviewId",
                table: "FmHouses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FmHouses_RentalOverviewId",
                table: "FmHouses",
                column: "RentalOverviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_FmHouses_RentalOverviews_RentalOverviewId",
                table: "FmHouses",
                column: "RentalOverviewId",
                principalTable: "RentalOverviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
