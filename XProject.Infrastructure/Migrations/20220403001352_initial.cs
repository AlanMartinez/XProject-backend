using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XProject.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurityId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Security_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "Security",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cuit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    MonthlyLimit = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuit_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cuit_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cuit_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FileIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CuitId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operation_Cuit_CuitId",
                        column: x => x.CuitId,
                        principalTable: "Cuit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PointOfSale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointOfSaleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CuitId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfSale_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointOfSale_Cuit_CuitId",
                        column: x => x.CuitId,
                        principalTable: "Cuit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlWsaa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlWS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginDn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationDn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Binding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfSaleId1 = table.Column<int>(type: "int", nullable: false),
                    PointOfSaleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connection_PointOfSale_PointOfSaleId",
                        column: x => x.PointOfSaleId,
                        principalTable: "PointOfSale",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Connection_PointOfSale_PointOfSaleId1",
                        column: x => x.PointOfSaleId1,
                        principalTable: "PointOfSale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connection_PointOfSaleId",
                table: "Connection",
                column: "PointOfSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Connection_PointOfSaleId1",
                table: "Connection",
                column: "PointOfSaleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cuit_AddressId",
                table: "Cuit",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuit_ContactId",
                table: "Cuit",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuit_UserId",
                table: "Cuit",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_CuitId",
                table: "Operation",
                column: "CuitId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfSale_AddressId",
                table: "PointOfSale",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointOfSale_CuitId",
                table: "PointOfSale",
                column: "CuitId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SecurityId",
                table: "User",
                column: "SecurityId",
                unique: true,
                filter: "[SecurityId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connection");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "PointOfSale");

            migrationBuilder.DropTable(
                name: "Cuit");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Security");
        }
    }
}
