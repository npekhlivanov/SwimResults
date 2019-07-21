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
        }
    }
}
