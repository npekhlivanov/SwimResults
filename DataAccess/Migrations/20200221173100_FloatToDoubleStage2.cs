using Microsoft.EntityFrameworkCore.Migrations;
using NP.Helpers;

namespace DataAccess.Migrations
{
    public partial class FloatToDoubleStage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ValidateNotNull(nameof(migrationBuilder));
            migrationBuilder.AlterColumn<double>(
                name: "StrokeCount",
                table: "WorkoutIntervals",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Swolf",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount end",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComputedColumnSql: "cast (0 as real)");

            migrationBuilder.AlterColumn<double>(
                name: "Pace",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComputedColumnSql: "cast (0 as real)");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(Id)",
                oldClrType: typeof(float),
                oldType: "real",
                oldComputedColumnSql: "cast (0 as real)");

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDistance(Id)",
                oldClrType: typeof(float),
                oldType: "real",
                oldComputedColumnSql: "cast (0 as real)");

            migrationBuilder.Sql("update WorkoutIntervalLengths set Duration = ROUND(Duration, 1)");
            migrationBuilder.Sql("update WorkoutIntervals set TimeOffset=ROUND(TimeOffset,1)");
            migrationBuilder.Sql("update WorkoutIntervals set StrokeCount=(select sum(StrokeCount)/cast(count(Id) as float) from WorkoutIntervalLengths L where L.WorkoutIntervalId = WorkoutIntervals.Id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ValidateNotNull(nameof(migrationBuilder));
            migrationBuilder.AlterColumn<float>(
                name: "StrokeCount",
                table: "WorkoutIntervals",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                type: "real",
                nullable: true,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(double),
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount end");

            migrationBuilder.AlterColumn<float>(
                name: "Pace",
                table: "WorkoutIntervals",
                type: "real",
                nullable: true,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(double),
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                type: "real",
                nullable: false,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(double),
                oldComputedColumnSql: "dbo.fnGetIntervalDuration(Id)");

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                type: "real",
                nullable: false,
                computedColumnSql: "cast (0 as real)",
                oldClrType: typeof(double),
                oldComputedColumnSql: "dbo.fnGetIntervalDistance(Id)");
        }
    }
}
