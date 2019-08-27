using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class IntervalNo_LengthNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervalNo",
                table: "WorkoutIntervals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LengthNo",
                table: "WorkoutIntervalLengths",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount end",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDuration(id) * 50 / dbo.fnGetIntervalDistance(id) + StrokeCount");

            migrationBuilder.AlterColumn<int>(
                name: "StrokeTypeId",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "dbo.fnGetIntervalStrokeType(Id)",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComputedColumnSql: "dbo.fnGetIntervalStrokeType(id)");

            migrationBuilder.AlterColumn<float>(
                name: "Pace",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDuration(id) * 100 / dbo.fnGetIntervalDistance(id)");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(Id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDuration(id)");

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDistance(Id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDistance(id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalNo",
                table: "WorkoutIntervals");

            migrationBuilder.DropColumn(
                name: "LengthNo",
                table: "WorkoutIntervalLengths");

            migrationBuilder.AlterColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(id) * 50 / dbo.fnGetIntervalDistance(id) + StrokeCount",
                oldClrType: typeof(float),
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount end");

            migrationBuilder.AlterColumn<int>(
                name: "StrokeTypeId",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "dbo.fnGetIntervalStrokeType(id)",
                oldClrType: typeof(int),
                oldNullable: true,
                oldComputedColumnSql: "dbo.fnGetIntervalStrokeType(Id)");

            migrationBuilder.AlterColumn<float>(
                name: "Pace",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(id) * 100 / dbo.fnGetIntervalDistance(id)",
                oldClrType: typeof(float),
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDuration(Id)");

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDistance(id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDistance(Id)");
        }
    }
}
