using Microsoft.EntityFrameworkCore.Migrations;
using NP.Helpers;

namespace DataAccess.Migrations
{
    public partial class FloatToDoubleStage1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ValidateNotNull(nameof(migrationBuilder));
            migrationBuilder.AlterColumn<double>(
                name: "TimeOffset",
                table: "WorkoutIntervals",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "WorkoutIntervalLengths",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "WorkoutIntervalLengths",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount * 2 end");

            migrationBuilder.AlterColumn<float>(
                name: "Pace",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(float),
                oldType: "real",
                oldComputedColumnSql: "dbo.fnGetIntervalDuration(Id)");

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(float),
                oldType: "real",
                oldComputedColumnSql: "dbo.fnGetIntervalDistance(Id)");

            var alterFunc =
              @"ALTER FUNCTION [dbo].[fnGetIntervalDuration] (@workoutItervalId int) RETURNS float(53) AS " +
              "BEGIN " +
              "DECLARE @ret float(53); " +
              "select @ret = sum(Duration) from WorkoutIntervalLengths where WorkoutIntervalId=@workoutItervalId; " +
              "IF (@ret IS NULL) SET @ret = 0; " +
              "RETURN @ret; " +
              "END";
            migrationBuilder.Sql(alterFunc);

            alterFunc =
                @"ALTER FUNCTION [dbo].[fnGetIntervalDistance] (@workoutItervalId int) RETURNS float(53) AS " +
                "BEGIN " +
                "DECLARE @ret float(53); " +
                "select @ret = sum(Distance) from WorkoutIntervalLengths where WorkoutIntervalId=@workoutItervalId; " +
                "IF (@ret IS NULL) SET @ret = 0; " +
                "RETURN @ret; " +
                "END";
            migrationBuilder.Sql(alterFunc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ValidateNotNull(nameof(migrationBuilder));

            var alterFunc =
              @"ALTER FUNCTION [dbo].[fnGetIntervalDuration] (@workoutItervalId int) RETURNS real AS " +
              "BEGIN " +
              "DECLARE @ret real; " +
              "select @ret = sum(Duration) from WorkoutIntervalLengths where WorkoutIntervalId=@workoutItervalId; " +
              "IF (@ret IS NULL) SET @ret = 0; " +
              "RETURN @ret; " +
              "END";
            migrationBuilder.Sql(alterFunc);

            alterFunc =
                @"ALTER FUNCTION [dbo].[fnGetIntervalDistance] (@workoutItervalId int) RETURNS real AS " +
                "BEGIN " +
                "DECLARE @ret real; " +
                "select @ret = sum(Distance) from WorkoutIntervalLengths where WorkoutIntervalId=@workoutItervalId; " +
                "IF (@ret IS NULL) SET @ret = 0; " +
                "RETURN @ret; " +
                "END";
            migrationBuilder.Sql(alterFunc);

            migrationBuilder.AlterColumn<float>(
                name: "TimeOffset",
                table: "WorkoutIntervals",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervalLengths",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervalLengths",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                type: "real",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount * 2 end",
                oldClrType: typeof(float),
                oldNullable: true,
                oldComputedColumnSql: "cast (0 as real)");

            migrationBuilder.AlterColumn<float>(
                name: "Pace",
                table: "WorkoutIntervals",
                type: "real",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end",
                oldClrType: typeof(float),
                oldNullable: true,
                oldComputedColumnSql: "cast (0 as real)");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                type: "real",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(Id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "cast (0 as real)");

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                type: "real",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDistance(Id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "cast (0 as real)");
        }
    }
}
