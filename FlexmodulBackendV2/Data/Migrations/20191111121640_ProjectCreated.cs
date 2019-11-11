using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexmodulBackendV2.Data.Migrations
{
    public partial class ProjectCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyTown = table.Column<string>(nullable: true),
                    CompanyStreet = table.Column<string>(nullable: true),
                    CompanyPostalCode = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "FMHouseType",
                columns: table => new
                {
                    FMHouseTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HouseType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMHouseType", x => x.FMHouseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HouseSection = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Supplier = table.Column<string>(nullable: true),
                    Units = table.Column<string>(nullable: true),
                    PricePerUnit = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<string>(nullable: false),
                    JwtId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Used = table.Column<bool>(nullable: false),
                    Invalidated = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentalOverview",
                columns: table => new
                {
                    RentalOverviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseStatus = table.Column<int>(nullable: false),
                    SetupAddressTown = table.Column<string>(nullable: true),
                    SetupAddressStreet = table.Column<string>(nullable: true),
                    SetupAddressPostalCode = table.Column<int>(nullable: false),
                    EstimatedPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalOverview", x => x.RentalOverviewId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    AuthenticationLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MaterialOnHouseType",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false),
                    FMHouseTypeId = table.Column<int>(nullable: false),
                    MaterialAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialOnHouseType", x => new { x.FMHouseTypeId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_MaterialOnHouseType_FMHouseType_FMHouseTypeId",
                        column: x => x.FMHouseTypeId,
                        principalTable: "FMHouseType",
                        principalColumn: "FMHouseTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialOnHouseType_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FMHouse",
                columns: table => new
                {
                    FMHouseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HouseTypeFMHouseTypeId = table.Column<int>(nullable: false),
                    SquareMeters = table.Column<int>(nullable: false),
                    RentalOverviewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMHouse", x => x.FMHouseId);
                    table.ForeignKey(
                        name: "FK_FMHouse_FMHouseType_HouseTypeFMHouseTypeId",
                        column: x => x.HouseTypeFMHouseTypeId,
                        principalTable: "FMHouseType",
                        principalColumn: "FMHouseTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FMHouse_RentalOverview_RentalOverviewId",
                        column: x => x.RentalOverviewId,
                        principalTable: "RentalOverview",
                        principalColumn: "RentalOverviewId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionInformation",
                columns: table => new
                {
                    ProductionInformationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HouseId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ExteriorWalls = table.Column<int>(nullable: true),
                    Ventilation = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ProductionPrice = table.Column<int>(nullable: false),
                    ProductionDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedByUserId = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RentalOverviewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionInformation", x => x.ProductionInformationId);
                    table.ForeignKey(
                        name: "FK_ProductionInformation_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionInformation_FMHouse_HouseId",
                        column: x => x.HouseId,
                        principalTable: "FMHouse",
                        principalColumn: "FMHouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionInformation_User_LastUpdatedByUserId",
                        column: x => x.LastUpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionInformation_RentalOverview_RentalOverviewId",
                        column: x => x.RentalOverviewId,
                        principalTable: "RentalOverview",
                        principalColumn: "RentalOverviewId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalCosts",
                columns: table => new
                {
                    AdditionalCostsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    ProductionInformationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCosts", x => x.AdditionalCostsId);
                    table.ForeignKey(
                        name: "FK_AdditionalCosts_ProductionInformation_ProductionInformationId",
                        column: x => x.ProductionInformationId,
                        principalTable: "ProductionInformation",
                        principalColumn: "ProductionInformationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    RentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HouseProductionInfoProductionInformationId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    InsurancePrice = table.Column<float>(nullable: false),
                    RentPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.RentId);
                    table.ForeignKey(
                        name: "FK_Rent_ProductionInformation_HouseProductionInfoProductionInformationId",
                        column: x => x.HouseProductionInfoProductionInformationId,
                        principalTable: "ProductionInformation",
                        principalColumn: "ProductionInformationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalCosts_ProductionInformationId",
                table: "AdditionalCosts",
                column: "ProductionInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_FMHouse_HouseTypeFMHouseTypeId",
                table: "FMHouse",
                column: "HouseTypeFMHouseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FMHouse_RentalOverviewId",
                table: "FMHouse",
                column: "RentalOverviewId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOnHouseType_MaterialId",
                table: "MaterialOnHouseType",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionInformation_CustomerId",
                table: "ProductionInformation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionInformation_HouseId",
                table: "ProductionInformation",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionInformation_LastUpdatedByUserId",
                table: "ProductionInformation",
                column: "LastUpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionInformation_RentalOverviewId",
                table: "ProductionInformation",
                column: "RentalOverviewId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_HouseProductionInfoProductionInformationId",
                table: "Rent",
                column: "HouseProductionInfoProductionInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalCosts");

            migrationBuilder.DropTable(
                name: "MaterialOnHouseType");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "ProductionInformation");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "FMHouse");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "FMHouseType");

            migrationBuilder.DropTable(
                name: "RentalOverview");
        }
    }
}
