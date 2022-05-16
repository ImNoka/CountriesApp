using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestEFAsyncWPF.Migrations
{
    public partial class InitialCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EconomicUnions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicUnions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryUnions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryUnions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Capital = table.Column<string>(type: "TEXT", nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContinentId = table.Column<int>(type: "INTEGER", nullable: false),
                    MeetInteractionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryCountry",
                columns: table => new
                {
                    ConflictsId = table.Column<int>(type: "INTEGER", nullable: false),
                    OpenCountriesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCountry", x => new { x.ConflictsId, x.OpenCountriesId });
                    table.ForeignKey(
                        name: "FK_CountryCountry_Countries_ConflictsId",
                        column: x => x.ConflictsId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCountry_Countries_OpenCountriesId",
                        column: x => x.OpenCountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryEconomicUnion",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    EconomicUnionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryEconomicUnion", x => new { x.CountriesId, x.EconomicUnionsId });
                    table.ForeignKey(
                        name: "FK_CountryEconomicUnion_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryEconomicUnion_EconomicUnions_EconomicUnionsId",
                        column: x => x.EconomicUnionsId,
                        principalTable: "EconomicUnions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryMilitaryUnion",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    MilitaryUnionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMilitaryUnion", x => new { x.CountriesId, x.MilitaryUnionsId });
                    table.ForeignKey(
                        name: "FK_CountryMilitaryUnion_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryMilitaryUnion_MilitaryUnions_MilitaryUnionsId",
                        column: x => x.MilitaryUnionsId,
                        principalTable: "MilitaryUnions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GDPs",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GDPs", x => x.CountryId);
                    table.ForeignKey(
                        name: "FK_GDPs_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Product = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: true),
                    CountrySellerId = table.Column<int>(type: "INTEGER", nullable: true),
                    CountryBuyerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    MeetCountryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Countries_CountryBuyerId",
                        column: x => x.CountryBuyerId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interactions_Countries_CountrySellerId",
                        column: x => x.CountrySellerId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interactions_Countries_MeetCountryId",
                        column: x => x.MeetCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentId",
                table: "Countries",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_MeetInteractionId",
                table: "Countries",
                column: "MeetInteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCountry_OpenCountriesId",
                table: "CountryCountry",
                column: "OpenCountriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryEconomicUnion_EconomicUnionsId",
                table: "CountryEconomicUnion",
                column: "EconomicUnionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryMilitaryUnion_MilitaryUnionsId",
                table: "CountryMilitaryUnion",
                column: "MilitaryUnionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_CountryBuyerId",
                table: "Interactions",
                column: "CountryBuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_CountrySellerId",
                table: "Interactions",
                column: "CountrySellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_MeetCountryId",
                table: "Interactions",
                column: "MeetCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Interactions_MeetInteractionId",
                table: "Countries",
                column: "MeetInteractionId",
                principalTable: "Interactions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Continents_ContinentId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Interactions_MeetInteractionId",
                table: "Countries");

            migrationBuilder.DropTable(
                name: "CountryCountry");

            migrationBuilder.DropTable(
                name: "CountryEconomicUnion");

            migrationBuilder.DropTable(
                name: "CountryMilitaryUnion");

            migrationBuilder.DropTable(
                name: "GDPs");

            migrationBuilder.DropTable(
                name: "EconomicUnions");

            migrationBuilder.DropTable(
                name: "MilitaryUnions");

            migrationBuilder.DropTable(
                name: "Continents");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
