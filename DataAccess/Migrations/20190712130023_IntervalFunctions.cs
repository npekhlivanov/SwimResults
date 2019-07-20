using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class IntervalFunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Consider using SQL statements instead of scalar functions:
            // https://www.mssqltips.com/sqlservertip/5464/computed-columns-with-scalar-functions-sql-server-performance-issue/

            var addFunc =
               @"CREATE FUNCTION [dbo].[fnGetIntervalDuration] (@workoutItervalId int) RETURNS real AS " +
               "BEGIN " +
               "DECLARE @ret real; " +
               "select @ret = sum(Duration) from WorkoutIntervalLengths where WorkoutIntervalId=@workoutItervalId; " +
               "IF (@ret IS NULL) SET @ret = 0; " +
               "RETURN @ret; " +
               "END";
            migrationBuilder.Sql(addFunc);

            addFunc =
                @"CREATE FUNCTION [dbo].[fnGetIntervalDistance] (@workoutItervalId int) RETURNS real AS " +
                "BEGIN " +
                "DECLARE @ret real; " +
                "select @ret = sum(Distance) from WorkoutIntervalLengths where WorkoutIntervalId=@workoutItervalId; " +
                "IF (@ret IS NULL) SET @ret = 0; " +
                "RETURN @ret; " +
                "END";
            migrationBuilder.Sql(addFunc);

            migrationBuilder.AddColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDistance(id)");

            migrationBuilder.AddColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "WorkoutIntervals");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "WorkoutIntervals");
            
            migrationBuilder.Sql(@"drop function [dbo].[fnGetIntervalDuration]");
            migrationBuilder.Sql(@"drop function [dbo].[fnGetIntervalDistance]");
        }
    }
}
