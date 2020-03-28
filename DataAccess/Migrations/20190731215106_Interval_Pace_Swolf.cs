using System;
using Constants;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Interval_Pace_Swolf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

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
                values: new[]
                {
                    new { Id = WorkoutIntervalTypes.IntermediateQuickFreestyleId, Name = "Intermediate quick freestyle" },
                    new { Id = WorkoutIntervalTypes.FinalQuickFreestyle2Id, Name = "Final quick freestyle 2" },
                    new { Id = WorkoutIntervalTypes.OtherkFreestyleId, Name = "Other freestyle" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

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
