using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerAggregator.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegionName",
                table: "Statistics",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_RegionName_MonthDate",
                table: "Statistics",
                columns: new[] { "RegionName", "MonthDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statistics_RegionName_MonthDate",
                table: "Statistics");

            migrationBuilder.AlterColumn<string>(
                name: "RegionName",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
