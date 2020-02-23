using Microsoft.EntityFrameworkCore.Migrations;
using NP.Helpers;

namespace DataAccess.Migrations
{
    public partial class FloatToDoubleStage3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ValidateNotNull(nameof(migrationBuilder));
            migrationBuilder.AlterColumn<double>(
                name: "Pace",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "CourseLength",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "ActiveTime",
                table: "Workouts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.Sql("Workouts set Duration=ROUND(Duration,1), ActiveTime=ROUND(ActiveTime,1), Pace=ActiveTime*100/Distance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ValidateNotNull(nameof(migrationBuilder));
            migrationBuilder.AlterColumn<float>(
                name: "Pace",
                table: "Workouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "Workouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Distance",
                table: "Workouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "CourseLength",
                table: "Workouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "ActiveTime",
                table: "Workouts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
