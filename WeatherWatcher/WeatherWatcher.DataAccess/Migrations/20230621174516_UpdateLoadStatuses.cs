using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherWatcher.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoadStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherInfos_LoadArchiveStatus_LoadArchiveStatusId",
                table: "WeatherInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoadArchiveStatus",
                table: "LoadArchiveStatus");

            migrationBuilder.RenameTable(
                name: "LoadArchiveStatus",
                newName: "LoadArchiveStatuses");

            migrationBuilder.AlterColumn<double>(
                name: "WindTemperature",
                table: "WeatherInfos",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "DewPointTemperature",
                table: "WeatherInfos",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "LoadArchiveStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowsCount",
                table: "LoadArchiveStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoadArchiveStatuses",
                table: "LoadArchiveStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherInfos_LoadArchiveStatuses_LoadArchiveStatusId",
                table: "WeatherInfos",
                column: "LoadArchiveStatusId",
                principalTable: "LoadArchiveStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherInfos_LoadArchiveStatuses_LoadArchiveStatusId",
                table: "WeatherInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoadArchiveStatuses",
                table: "LoadArchiveStatuses");

            migrationBuilder.DropColumn(
                name: "Error",
                table: "LoadArchiveStatuses");

            migrationBuilder.DropColumn(
                name: "RowsCount",
                table: "LoadArchiveStatuses");

            migrationBuilder.RenameTable(
                name: "LoadArchiveStatuses",
                newName: "LoadArchiveStatus");

            migrationBuilder.AlterColumn<int>(
                name: "WindTemperature",
                table: "WeatherInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "DewPointTemperature",
                table: "WeatherInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoadArchiveStatus",
                table: "LoadArchiveStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherInfos_LoadArchiveStatus_LoadArchiveStatusId",
                table: "WeatherInfos",
                column: "LoadArchiveStatusId",
                principalTable: "LoadArchiveStatus",
                principalColumn: "Id");
        }
    }
}
