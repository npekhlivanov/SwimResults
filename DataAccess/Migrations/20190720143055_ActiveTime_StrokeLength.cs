using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ActiveTime_StrokeLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ActiveTime",
                table: "Workouts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CourseLength",
                table: "Workouts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StrokeCount",
                table: "WorkoutIntervals",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDuration(id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "select dbo.fnGetIntervalDuration(id)");

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "dbo.fnGetIntervalDistance(id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "select dbo.fnGetIntervalDistance(id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTime",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "CourseLength",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "StrokeCount",
                table: "WorkoutIntervals");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "select dbo.fnGetIntervalDuration(id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDuration(id)");

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "WorkoutIntervals",
                nullable: false,
                computedColumnSql: "select dbo.fnGetIntervalDistance(id)",
                oldClrType: typeof(float),
                oldComputedColumnSql: "dbo.fnGetIntervalDistance(id)");
        }
    }
}
