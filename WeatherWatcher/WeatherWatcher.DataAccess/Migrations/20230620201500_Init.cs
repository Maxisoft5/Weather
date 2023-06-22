using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherWatcher.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoadArchiveStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadStatus = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadArchiveStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WindTemperature = table.Column<int>(type: "int", nullable: false),
                    RelativeHumidity = table.Column<int>(type: "int", nullable: false),
                    DewPointTemperature = table.Column<int>(type: "int", nullable: false),
                    AtmospherePressure = table.Column<int>(type: "int", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    Cloudiness = table.Column<int>(type: "int", nullable: false),
                    BottomLineCloudiness = table.Column<int>(type: "int", nullable: false),
                    HorizontalVisibility = table.Column<int>(type: "int", nullable: false),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadArchiveStatusId = table.Column<long>(type: "bigint", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherInfos_LoadArchiveStatus_LoadArchiveStatusId",
                        column: x => x.LoadArchiveStatusId,
                        principalTable: "LoadArchiveStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherInfos_LoadArchiveStatusId",
                table: "WeatherInfos",
                column: "LoadArchiveStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherInfos");

            migrationBuilder.DropTable(
                name: "LoadArchiveStatus");
        }
    }
}
