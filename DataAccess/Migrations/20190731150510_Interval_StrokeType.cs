using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Interval_StrokeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            var addFunc =
              "CREATE FUNCTION dbo.fnGetIntervalStrokeType (@workoutIntervalId int) RETURNS int AS\r\n " +
              "BEGIN\r\n" +
              "DECLARE @ret int;\r\n" +
              "select @ret = (select top 1 StrokeTypeId from WorkoutIntervalLengths where WorkoutIntervalId=@workoutIntervalId);\r\n" +
              "IF (@ret IS NULL) SET @ret = 0;\r\n" +
              "RETURN @ret;\r\n" +
              "END";
            migrationBuilder.Sql(addFunc);

            migrationBuilder.AddColumn<int>(
                name: "StrokeTypeId",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "dbo.fnGetIntervalStrokeType(id)");

            migrationBuilder.InsertData(
                table: "WorkoutIntervalType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 12, "Pre warm-up" });
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
                keyValue: 12);

            migrationBuilder.DropColumn(
                name: "StrokeTypeId",
                table: "WorkoutIntervals");

            migrationBuilder.Sql("drop function dbo.fnGetIntervalStrokeType");
        }
    }
}
