using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Interval_Pace_Swolf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Pace",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(id) * 100 / dbo.fnGetIntervalDistance(id)");

            migrationBuilder.AddColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(id) * 50 / dbo.fnGetIntervalDistance(id) + StrokeCount");

            migrationBuilder.InsertData(
                table: "WorkoutIntervalType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 13, "Intermediate quick freestyle" });

            migrationBuilder.InsertData(
                table: "WorkoutIntervalType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 14, "Final quick freestyle 2" });

            migrationBuilder.InsertData(
                table: "WorkoutIntervalType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 15, "Other freestyle" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkoutIntervalType",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "WorkoutIntervalType",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "WorkoutIntervalType",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DropColumn(
                name: "Pace",
                table: "WorkoutIntervals");

            migrationBuilder.DropColumn(
                name: "Swolf",
                table: "WorkoutIntervals");
        }
    }
}
